using RestSharp;
using SevenKnightsAI.Classes;
using SevenKnightsAI.Classes.Extensions;
using SevenKnightsAI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Media;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace SevenKnightsAI
{
	public partial class MainForm : Form
	{
		#region Private Fields

		private readonly CheckBox[][] _formationCheckBoxes = new CheckBox[2][];

		private readonly Point[][] _formationPositions = new Point[][]
		{
			new Point[]
			{
				new Point(46, 24),
				new Point(46, 44),
				new Point(25, 13),
				new Point(25, 33),
				new Point(25, 53)
			},
			new Point[]
			{
				new Point(46, 13),
				new Point(46, 33),
				new Point(46, 53),
				new Point(25, 24),
				new Point(25, 44)
			},
			new Point[]
			{
				new Point(46, 33),
				new Point(25, 7),
				new Point(25, 24),
				new Point(25, 41),
				new Point(25, 58)
			},
			new Point[]
			{
				new Point(46, 7),
				new Point(46, 24),
				new Point(46, 41),
				new Point(46, 58),
				new Point(25, 33)
			}
		};

		private readonly List<Button>[] _skillButtons = new List<Button>[7];

		private readonly Color COLOR_SEQUENCE_ERROR = Color.FromArgb(255, 127, 123);

		private readonly Color COLOR_SEQUENCE_OK = Color.FromArgb(178, 209, 255);

		private readonly Color COLOR_SKILL_LOOP = Color.FromArgb(94, 170, 255);

		private SevenKnightsCore AI;

		private AIProfiles AIProfiles;

		private SoundPlayer AlertSound;

		private bool LG_autoScroll = true;

		private bool loaded;

		private bool started;

		private SynchronizationContext SynchronizationContext;

		private BackgroundWorker Worker;
		#endregion Private Fields

		#region Public Properties

		public static MainForm Instance
		{
			get;

			private set;
		}

		public AISettings AISettings
		{
			get
			{
				return this.AIProfiles.CurrentProfile;
			}
		}

		public bool IsElevated
		{
			get
			{
				return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
			}
		}

        #endregion Public Properties

        #region Public Constructors

        public MainForm()
		{
			this.InitializeComponent();
			this.Init();
			this.ReloadTabs(true);
			if (!this.IsElevated)
			{
				this.adminToolStripLabel.Visible = false;
				this.AppendWarning("No admin permissions. AI might not function as expected!");
			}
		}

		#endregion Public Constructors

		#region Public Methods

		public void InvokeReloadTabs(bool refreshSettings)
		{
			base.Invoke(new MethodInvoker(delegate
			{
				this.ReloadTabs(refreshSettings);
			}));
		}

        #endregion Public Methods

        #region Private Methods

        private void AD_continuousCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			this.AISettings.AD_Continuous = checkBox.Checked;
		}

		private void AD_difficultyComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			this.AISettings.AD_Difficulty = comboBox.SelectedValue as Difficulty? ?? Difficulty.None;
		}

		private void AD_elementHeroesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			this.AISettings.AD_ElementHeroesOnly = checkBox.Checked;
		}

		private void AD_manageHeroHelpLabel_Click(object sender, EventArgs e)
		{
			Point location = this.AD_formationPanel.Parent.PointToScreen(this.AD_formationPanel.Location);
			location.X += 10;
			location.Y += 10;
			Help.ShowPopup(this.AD_formationPanel, "Choose the positions to manage when hero reached level 30", location);
		}

		private void AD_PopulateStage(int stages)
		{
			int selectedIndex = this.AD_stageComboBox.SelectedIndex;
			this.AD_stageComboBox.Items.Clear();
			for (int i = 0; i < stages; i++)
			{
				string item = (i + 1).ToString();
				this.AD_stageComboBox.Items.Add(item);
			}
			int num = this.AD_stageComboBox.Items.Count - 1;
			if (selectedIndex > num)
			{
				this.AD_stageComboBox.SelectedIndex = num;
				return;
			}
			this.AD_stageComboBox.SelectedIndex = selectedIndex;
		}

		private void AD_PopulateDifficulty(params Difficulty[] enabledDifficulties)
		{
			Dictionary<Difficulty, string> items = new Dictionary<Difficulty, string>();

			List<Difficulty> difficulties = new List<Difficulty>(enabledDifficulties);
			if (difficulties.Contains(Difficulty.None))
				items.Add(Difficulty.None, "---");
			if (difficulties.Contains(Difficulty.Easy))
				items.Add(Difficulty.Easy, Difficulty.Easy.ToString());
			if (difficulties.Contains(Difficulty.Normal))
				items.Add(Difficulty.Normal, Difficulty.Normal.ToString());
			if (difficulties.Contains(Difficulty.Hard))
				items.Add(Difficulty.Hard, Difficulty.Hard.ToString());

			Difficulty selectedDifficulty = this.AD_difficultyComboBox.SelectedValue as Difficulty? ?? Difficulty.None;
			if (!difficulties.Contains(selectedDifficulty))
				selectedDifficulty = Difficulty.None;

			this.AD_difficultyComboBox.SelectedValueChanged -= this.AD_difficultyComboBox_SelectedValueChanged;
			this.AD_difficultyComboBox.DisplayMember = "Value";
			this.AD_difficultyComboBox.ValueMember = "Key";
			this.AD_difficultyComboBox.DataSource = new BindingSource(items, null);
			this.AD_difficultyComboBox.SelectedValueChanged += this.AD_difficultyComboBox_SelectedValueChanged;

			this.AD_difficultyComboBox.SelectedValue = selectedDifficulty;
		}

        private void AD_posCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			List<int> list = new List<int>();
			CheckBox[] array = this._formationCheckBoxes[0];
			for (int i = 0; i < array.Length; i++)
			{
				CheckBox checkBox = array[i];
				if (checkBox.Checked)
				{
					list.Add((int)Convert.ToInt16(checkBox.Tag));
				}
			}
			this.AISettings.AD_HeroManagePositions = list.ToArray();
		}

		private void AD_sequenceButton_Click(object sender, EventArgs e)
		{
			this.AD_ShowSequencerForm();
		}

		private void AD_ShowSequencerForm()
		{
			StageSequencerForm stageSequencerForm = new StageSequencerForm(this.AISettings, this.started);
			stageSequencerForm.ShowDialog(this);
			this.AD_UpdateSequenceButton();
		}

		private void AD_stageComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			this.AISettings.AD_Stage = comboBox.SelectedIndex;
		}

		private void AD_StopOnFullHeroes_Checkbox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			this.AISettings.AD_StopOnFullHeroes = checkBox.Checked;
		}

		private void AD_UpdateSequenceButton()
		{
			if (!this.AD_sequenceButton.Enabled)
			{
				this.AD_sequenceButton.BackColor = Control.DefaultBackColor;
				return;
			}
			if (this.AISettings.AD_WorldSequence == null || this.AISettings.AD_WorldSequence.Length <= 0)
			{
				this.AD_sequenceButton.BackColor = this.COLOR_SEQUENCE_ERROR;
				return;
			}
			this.AD_sequenceButton.BackColor = this.COLOR_SEQUENCE_OK;
		}

		private void AD_worldComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			this.AD_stageComboBox.Enabled = (comboBox.SelectedIndex != 0 && comboBox.SelectedIndex != 1);
			this.AD_sequenceButton.Enabled = (comboBox.SelectedIndex == 1);
			this.AISettings.AD_World = (World)comboBox.SelectedIndex;
			this.AD_UpdateSequenceButton();
			if (this.AISettings.AD_World == World.None || this.AISettings.AD_World == World.Sequencer || this.AISettings.AD_World == World.MoonlitIsle)
			{
				this.AD_PopulateStage(20);
			}
			else if (this.AISettings.AD_World == World.WesternEmpire)
			{
				this.AD_PopulateStage(15);
			}
			else if (this.AISettings.AD_World == World.EasternEmpire)
			{
				this.AD_PopulateStage(10);
			}
			else
			{
				this.AD_PopulateStage(10);
			}
			if (this.AISettings.AD_World == World.MoonlitIsle || this.AISettings.AD_World == World.WesternEmpire || this.AISettings.AD_World == World.EasternEmpire)
			{
				this.AD_PopulateDifficulty(Difficulty.None, Difficulty.Easy, Difficulty.Normal);
			}
			else
			{
				this.AD_PopulateDifficulty(Difficulty.None, Difficulty.Easy, Difficulty.Normal, Difficulty.Hard);
			}
			if (this.loaded && this.AISettings.AD_World == World.Sequencer)
			{
				this.AD_ShowSequencerForm();
			}

		}

		private void aiButton_Click(object sender, EventArgs e)
		{
			if (this.AIProfiles.TMP_Paused)
			{
				this.ResumeAI();
				return;
			}
			if (!this.started)
			{
				this.StartAI();
				return;
			}
			this.StopAI();
		}

		private void aiPause_Click(object sender, EventArgs e)
		{
			this.PauseAI();
		}

		private void AppendLog(string message)
		{
			this.AppendLog(message, Color.Black);
		}

		private void AppendLog(string message, Color color)
		{
			RichTextBox lG_logTextBox = this.LG_logTextBox;
			string text = DateTime.Now.ToString("[hh:mm:ss tt]  ", CultureInfo.InvariantCulture);
			lG_logTextBox.AppendText(text, Color.Black);
			lG_logTextBox.AppendText(message, color);
			lG_logTextBox.AppendText(Environment.NewLine);
		}

		private void AppendWarning(string message)
		{
			this.AppendLog("WARNING: " + message, Color.Tan);
		}

		private void AR_useRubyCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			bool @checked = checkBox.Checked;
			this.AISettings.AR_UseRuby = @checked;
		}

		private void AR_useRubyNumericBox_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = sender as NumericUpDown;
			int aR_UseRubyAmount = Convert.ToInt32(numericUpDown.Value);
			this.AISettings.AR_UseRubyAmount = aR_UseRubyAmount;
		}

		private void BackgroundWorkerOnCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.started = false;
			this.aiButton.Text = "Start AI";
			this.statusToolStripLabel.Text = "Status: AI Stopped";
			this.ST_toggleBlueStacksButton.Enabled = false;
			try
			{
				Exception arg_39_0 = e.Error;
			}
			finally
			{
				this.Worker.Dispose();
			}
		}

		private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			ProgressArgs progressArgs = e.UserState as ProgressArgs;
			if (progressArgs == null)
			{
				return;
			}
			Label label = null;
			switch (progressArgs.Type)
			{
				case ProgressType.OBJECTIVE:
					this.statusToolStripLabel.Text = string.Format("Status: {0}", progressArgs.Message.ToString());
					this.UpdateCurrentProfileStatus();
					this.AppendLog("Changing objective to: " + progressArgs.Message.ToString(), Color.OrangeRed);
					return;

				case ProgressType.EVENT:
					this.AppendLog(progressArgs.Message.ToString(), progressArgs.Color);
					this.UpdateCurrentProfileStatus();
					return;

				case ProgressType.ERROR:
					this.AppendLog("ERROR: " + progressArgs.Message, Color.Firebrick);
					return;

				case ProgressType.WARNING:
					this.AppendWarning(progressArgs.Message.ToString());
					return;

				case ProgressType.COUNT:
					{
						Dictionary<string, object> dictionary = progressArgs.Message as Dictionary<string, object>;
						Objective objective = (Objective)dictionary["objective"];
						string arg = Utility.ToTitleCase(objective.ToString().Replace("_", " "));
						if (objective == Objective.ARENA)
						{
							int num = (int)dictionary["winCount"];
							int num2 = (int)dictionary["loseCount"];
							this.arenaCountLabel.Text = string.Format("{0} (Win/Lose): {1}/{2}", arg, num, num2);
							return;
						}

						int num3 = (int)dictionary["count"];
						string text = string.Format("{0}: {1}", arg, num3);
                        if (objective == Objective.HERO_MANAGEMENT)
                        {
                            string t1 = ""+dictionary["hc"];
                            string t2 = ""+ dictionary["hm"];
                            text = string.Format("H : {0} / {1}",t1 , t2);
                        }
                        switch (objective)
						{
							case Objective.ADVENTURE:
								this.adventureCountLabel.Text = text;
								return;

							case Objective.GOLD_CHAMBER:
								this.goldChamberCountLabel.Text = text;
								return;

							case Objective.ARENA:
								break;

							case Objective.RAID:
								this.raidCountLabel.Text = text;
								return;

                            case Objective.HERO_MANAGEMENT:
                                this.HeroCountLabel.Text = text;
                                return;

							default:
								return;
						}
						break;
					}
				case ProgressType.KEY:
					{
						Dictionary<string, object> dictionary = progressArgs.Message as Dictionary<string, object>;
						Objective objective = (Objective)dictionary["objective"];
						int num4 = (int)dictionary["keys"];
						string text2;
						if (num4 == -1)
						{
							text2 = "-";
						}
						else if (dictionary.ContainsKey("time"))
						{
							string arg2 = ((TimeSpan)dictionary["time"]).ToString("mm':'ss");
							text2 = string.Format("x{0} ({1})", num4, arg2);
						}
						else
						{
							text2 = string.Format("x{0}", num4);
						}
						switch (objective)
						{
							case Objective.ADVENTURE:
								label = this.adventureKeysLabel;
								break;

							case Objective.GOLD_CHAMBER:
								label = this.towerKeysLabel;
								break;

							case Objective.ARENA:
								label = this.arenaKeysLabel;
								break;
						}
						if (text2 != null)
						{
							label.Text = text2;
							return;
						}
						break;
					}
				case ProgressType.RESOURCE:
					{
						Dictionary<string, object> dictionary = progressArgs.Message as Dictionary<string, object>;
						ResourceType resourceType = (ResourceType)dictionary["resourceType"];
						int num5 = (int)dictionary["value"];
						string text2;
						if (num5 == -1)
						{
							text2 = "-";
						}
						else
						{
							text2 = num5.ToString("N0");
						}
						switch (resourceType)
						{
							case ResourceType.GOLD:
								label = this.goldLabel;
								break;

							case ResourceType.RUBY:
								label = this.rubyLabel;
								break;

							case ResourceType.HONOR:
								label = this.honorLabel;
								break;

							case ResourceType.TOPAZ:
								label = this.topazLabel;
								break;
						}
						if (text2 != null)
						{
							label.Text = text2;
						}
						break;
					}
				case ProgressType.CURSORPOS:
					{
						Point curpos = (Point)progressArgs.Message;
						this.tsslCursorPosition.Text = string.Format("X: {0}, Y: {1}", curpos.X, curpos.Y);
						break;
					}
				case ProgressType.Alert:
					{
						if ((string)progressArgs.Message == "Heroes Full")
						{
							if (this.AISettings.AD_StopOnFullHeroes)
							{
								this.PauseAI();
								this.AlertSound.PlayLooping();
								MessageBox.Show("Heroes Full");
								this.AlertSound.Stop();
							}
						}
						break;
					}
				default:
					return;
			}
		}

		private void contactUsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://github.com/Nulled-Daelus/SevenKnightsAI");
		}

		private void contactUsLinkLabel_TextChanged(object sender, EventArgs e)
		{
			LinkLabel linkLabel = sender as LinkLabel;
		}

		private void enableCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			short num = Convert.ToInt16(checkBox.Tag);
			bool @checked = checkBox.Checked;
			switch (num)
			{
				case 0:
					this.AISettings.AD_Enable = @checked;
					return;

				case 1:
					this.AISettings.GC_Enable = @checked;
					return;

				case 2:
					this.AISettings.AR_Enable = @checked;
					return;

				case 3:
					this.AISettings.RD_Enable = @checked;
					return;

				default:
					return;
			}
		}

		private void EnablePause(bool value)
		{
			ContextMenu contextMenu = this.aiButton.ContextMenu;
			MenuItem menuItem = contextMenu.MenuItems[0];
			menuItem.Enabled = value;
			aiPause.Enabled = value;
		}

		private Control FindControlByTag(Control.ControlCollection controls, int tag)
		{
			foreach (Control control in controls)
			{
				if (control.Tag != null && Convert.ToInt32(control.Tag) == tag)
				{
					return control;
				}
			}
			return null;
		}

		private void formationComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			int selectedIndex = comboBox.SelectedIndex;
			short tag = Convert.ToInt16(comboBox.Tag);
			this.SetFormation((int)tag, selectedIndex);
			if (selectedIndex != 0)
			{
				this.SetFormationPosition((int)tag, selectedIndex - 1);
				this.SetFormationVisibility((int)tag, true);
				return;
			}
			this.SetFormationVisibility((int)tag, false);
		}

		private void GB_WaitForKeys_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			bool @checked = checkBox.Checked;
			this.AISettings.GB_WaitForKeys = @checked;
		}

		private void Init()
		{
			MainForm.Instance = this;
			this.SynchronizationContext = SynchronizationContext.Current;
			HotTimeHelper.ImportHotTimeSchedule();
			try
			{
				this.AIProfiles = AIProfiles.Load();
			}
			catch (AISettingsException ex)
			{
				if (ex.ErrorCode != -1)
				{
					MessageBox.Show("Error loading settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				this.AIProfiles = new AIProfiles(new AISettings());
			}
			this.AI = new SevenKnightsCore(this.AIProfiles);
		}

		private void InitAdventureTab()
		{
			this._formationCheckBoxes[0] = new CheckBox[]
			{
				this.AD_pos1CheckBox,
				this.AD_pos2CheckBox,
				this.AD_pos3CheckBox,
				this.AD_pos4CheckBox,
				this.AD_pos5CheckBox
			};
			for (int i = 0; i < 3; i++)
			{
				this._skillButtons[i] = new List<Button>();
			}
			this.AD_enableCheckBox.Checked = this.AISettings.AD_Enable;
			this.AD_limitCheckBox.Checked = this.AISettings.AD_EnableLimit;
			this.AD_limitNumericBox.Value = this.AISettings.AD_Limit;
			this.AD_worldComboBox.SelectedIndex = (int)this.AISettings.AD_World;
			this.AD_stageComboBox.SelectedIndex = this.AISettings.AD_Stage;
			this.AD_difficultyComboBox.SelectedValue = this.AISettings.AD_Difficulty;
			this.AD_teamComboBox.SelectedIndex = (int)this.AISettings.AD_Team;
			this.AD_formationComboBox.SelectedIndex = (int)this.AISettings.AD_Formation;
			this.AD_continuousCheckBox.Checked = this.AISettings.AD_Continuous;
			this.AD_elementHeroesCheckBox.Checked = this.AISettings.AD_ElementHeroesOnly;
			this.AD_masteryComboBox.SelectedIndex = (int)this.AISettings.AD_Mastery;
			this.AD_StopOnFullHeroes_Checkbox.Checked = this.AISettings.AD_StopOnFullHeroes;
            this.AD_CheckingHeroes_Checkbox.Checked = this.AISettings.AD_CheckingHeroes;
			this.AD_wave1LoopCheckBox.Checked = this.AISettings.AD_Wave1Loop;
			this.AD_wave2LoopCheckBox.Checked = this.AISettings.AD_Wave2Loop;
			this.AD_wave3LoopCheckBox.Checked = this.AISettings.AD_Wave3Loop;
			if (this.AISettings.AD_HeroManagePositions != null)
			{
				int[] aD_HeroManagePositions = this.AISettings.AD_HeroManagePositions;
				for (int j = 0; j < aD_HeroManagePositions.Length; j++)
				{
					int num = aD_HeroManagePositions[j];
					CheckBox checkBox = this._formationCheckBoxes[0][num];
					checkBox.Checked = false;
					checkBox.Checked = true;
				}
			}
			switch (this.AISettings.AD_SkillType)
			{
				case SkillType.Auto:
					this.AD_autoSkillRadio.Checked = true;
					break;

				case SkillType.Manual:
					this.AD_manualSkillRadio.Checked = true;
					break;

				case SkillType.Both:
					this.AD_bothSkillRadio.Checked = true;
					break;
			}
			Panel[] wavePanels = new Panel[]
			{
				this.AD_wave1Panel,
				this.AD_wave2Panel,
				this.AD_wave3Panel
			};
			int[][] waveSkill = new int[][]
			{
				this.AISettings.AD_Wave1Skills,
				this.AISettings.AD_Wave2Skills,
				this.AISettings.AD_Wave3Skills
			};
			this.LoadWaveSkills(wavePanels, waveSkill, 0);
		}

		private void InitArenaTab()
		{
			this.AR_enableCheckBox.Checked = this.AISettings.AR_Enable;
			this.AR_limitCheckBox.Checked = this.AISettings.AR_EnableLimit;
			this.AR_limitNumericBox.Value = this.AISettings.AR_Limit;
			this.AR_useRubyCheckBox.Checked = this.AISettings.AR_UseRuby;
			this.AR_useRubyNumericBox.Value = this.AISettings.AR_UseRubyAmount;
			this.AR_masteryComboBox.SelectedIndex = (int)this.AISettings.AR_Mastery;
		}

		private void InitGlobalProfile()
		{
			this.GB_WaitForKeys.Checked = this.AISettings.GB_WaitForKeys;
		}

		private void InitGoldChamberTab()
		{
			this._formationCheckBoxes[1] = new CheckBox[]
			{
				this.GC_pos1CheckBox,
				this.GC_pos2CheckBox,
				this.GC_pos3CheckBox,
				this.GC_pos4CheckBox,
				this.GC_pos5CheckBox
			};
			for (int i = 3; i < 5; i++)
			{
				this._skillButtons[i] = new List<Button>();
			}
			this.GC_enableCheckBox.Checked = this.AISettings.GC_Enable;
			this.GC_limitCheckBox.Checked = this.AISettings.GC_EnableLimit;
			this.GC_limitNumericBox.Value = this.AISettings.GC_Limit;
			this.GC_teamComboBox.SelectedIndex = (int)this.AISettings.GC_Team;
			this.GC_formationComboBox.SelectedIndex = (int)this.AISettings.GC_Formation;
			this.GC_masteryComboBox.SelectedIndex = (int)this.AISettings.GC_Mastery;
			this.GC_wave1LoopCheckBox.Checked = this.AISettings.GC_Wave1Loop;
			this.GC_wave2LoopCheckBox.Checked = this.AISettings.GC_Wave2Loop;
			switch (this.AISettings.GC_SkillType)
			{
				case SkillType.Auto:
					this.GC_autoSkillRadio.Checked = true;
					break;

				case SkillType.Manual:
					this.GC_manualSkillRadio.Checked = true;
					break;

				case SkillType.Both:
					this.GC_bothSkillRadio.Checked = true;
					break;
			}
			Panel[] wavePanels = new Panel[]
			{
				this.GC_wave1Panel,
				this.GC_wave2Panel
			};
			int[][] waveSkill = new int[][]
			{
				this.AISettings.GC_Wave1Skills,
				this.AISettings.GC_Wave2Skills
			};
			this.LoadWaveSkills(wavePanels, waveSkill, 3);
		}

		private void InitLogsTab()
		{ }

		private void InitRaidTab()
		{
			for (int i = 5; i < 7; i++)
			{
				this._skillButtons[i] = new List<Button>();
			}
			this.RD_enableCheckBox.Checked = this.AISettings.RD_Enable;
			this.RD_limitCheckBox.Checked = this.AISettings.RD_EnableLimit;
			this.RD_limitNumericBox.Value = this.AISettings.RD_Limit;
			this.RD_DragonLimitCheckBox.Checked = this.AISettings.RD_EnableDragonLimit;
			this.RD_DragonLimitNumericBox.Value = this.AISettings.RD_DragonLimit;
			this.RD_team1LoopCheckBox.Checked = this.AISettings.RD_Team1Loop;
			this.RD_team2LoopCheckBox.Checked = this.AISettings.RD_Team2Loop;
			switch (this.AISettings.RD_SkillType)
			{
				case SkillType.Auto:
					this.RD_autoSkillRadio.Checked = true;
					break;

				case SkillType.Manual:
					this.RD_manualSkillRadio.Checked = true;
					break;

				case SkillType.Both:
					this.RD_bothSkillRadio.Checked = true;
					break;
			}
			Panel[] wavePanels = new Panel[]
			{
				this.RD_team1Panel,
				this.RD_team2Panel
			};
			int[][] waveSkill = new int[][]
			{
				this.AISettings.RD_Team1Skills,
				this.AISettings.RD_Team2Skills
			};
			this.LoadWaveSkills(wavePanels, waveSkill, 5);
		}

		private void InitResourcesTab()
		{
			this.RS_sellHeroesCheckBox.Checked = this.AISettings.RS_SellHeroes;
			this.RS_heroStarsComboBox.SelectedIndex = this.AISettings.RS_SellHeroStars - 1;
			this.RS_heroAmountNumericBox.Value = this.AISettings.RS_SellHeroAmount;
			this.RS_heroAllRadioButton.Checked = this.AISettings.RS_SellHeroAll;
			this.RS_heroAmountRadioButton.Checked = !this.AISettings.RS_SellHeroAll;
			this.RS_sellItemsCheckBox.Checked = this.AISettings.RS_SellItems;
			this.RS_itemStarsComboBox.SelectedIndex = this.AISettings.RS_SellItemStars - 1;
			this.RS_itemAmountNumericBox.Value = this.AISettings.RS_SellItemAmount;
			this.RS_itemAllRadioButton.Checked = this.AISettings.RS_SellItemAll;
			this.RS_itemAmountRadioButton.Checked = !this.AISettings.RS_SellItemAll;
			this.RS_inboxHonors.Checked = this.AISettings.RS_InboxHonors;
			this.RS_inboxKeys.Checked = this.AISettings.RS_InboxKeys;
			this.RS_inboxGold.Checked = this.AISettings.RS_InboxGold;
			this.RS_inboxRubies.Checked = this.AISettings.RS_InboxRubies;
			this.RS_inboxTickets.Checked = this.AISettings.RS_InboxTickets;
			this.RS_luckyBoxCheckBox.Checked = this.AISettings.RS_CollectLuckyBox;
			this.RS_luckyChestCheckBox.Checked = this.AISettings.RS_CollectLuckyChest;
			this.RS_specialQuestsDailyCheckBox.Checked = this.AISettings.RS_SpecialQuestsDaily;
			this.RS_specialQuestsWeeklyCheckBox.Checked = this.AISettings.RS_SpecialQuestsWeekly;
			this.RS_specialQuestsMonthlyCheckBox.Checked = this.AISettings.RS_SpecialQuestsMonthly;
			this.RS_questsBattleCheckBox.Checked = this.AISettings.RS_QuestsBattle;
			this.RS_questsHeroCheckBox.Checked = this.AISettings.RS_QuestsHero;
			this.RS_questsItemCheckBox.Checked = this.AISettings.RS_QuestsItem;
			this.RS_questsSocialCheckBox.Checked = this.AISettings.RS_QuestsSocial;
			this.RS_sendHonorsFacebook.Checked = this.AISettings.RS_SendHonorsFacebook;
			this.RS_sendHonorsInGame.Checked = this.AISettings.RS_SendHonorsInGame;
			this.RS_buyKeyHonorsCheckBox.Checked = this.AISettings.RS_BuyKeyHonors;
			this.RS_buyKeyHonorsComboBox.SelectedIndex = (int)this.AISettings.RS_BuyKeyHonorsType;
			this.RS_buyKeyHonorsNumericBox.Value = this.AISettings.RS_BuyKeyHonorsAmount;
			this.RS_buyKeyRubiesCheckBox.Checked = this.AISettings.RS_BuyKeyRubies;
			this.RS_buyKeyRubiesComboBox.SelectedIndex = (int)this.AISettings.RS_BuyKeyRubiesType;
			this.RS_buyKeyRubiesNumericBox.Value = this.AISettings.RS_BuyKeyRubiesAmount;
		}

		private void InitSettingsTab()
		{
			this.ST_RefreshProfiles();
			this.ST_delayTrackBar.Value = this.AIProfiles.ST_Delay;
			this.ST_reconnectInterruptCheckBox.Checked = this.AIProfiles.ST_ReconnectInterruptEnable;
			this.ST_reconnectNumericUpDown.Value = this.AIProfiles.ST_ReconnectInterruptInterval;
			this.ST_hotTimeProfileCheckBox.Checked = this.AIProfiles.ST_EnableHotTimeProfile;
			this.ST_forceActiveCheckBox.Checked = this.AIProfiles.ST_BlueStacksForceActive;
			this.ST_pushbulletCheckBox.Checked = this.AIProfiles.ST_PushbulletEnable;
			this.ST_pushbulletEmailTextBox.Text = this.AIProfiles.ST_PushbulletEmail;
			this.ST_foregroundMode.Checked = this.AIProfiles.ST_ForegroundMode;
		}

		private void LG_clearButton_Click(object sender, EventArgs e)
		{
			this.LG_logTextBox.Clear();
			this.LG_logTextBox.Refresh();
		}

		private void LG_exportButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = string.Format("{0}.log", "sevenknights-".AppendTimeStamp());
			saveFileDialog.Filter = "Log files (*.log)|*.log|All files (*.*)|*.*";
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.InitialDirectory = Application.StartupPath;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
				{
					streamWriter.Write(this.LG_logTextBox.Text);
				}
			}
		}

		private void LG_LogPixel_Click(object sender, EventArgs e)
		{
			this.LogPixel();
		}

		private void LG_logTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.LG_autoScroll)
			{
				this.LG_ScrollToEnd();
			}
		}

        private void LG_SaveScreen_Click(object sender, EventArgs e) {
            Bitmap screen = this.AI.BlueStacks.CaptureFrame(!this.AIProfiles.ST_ForegroundMode);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "bmp";
            saveFileDialog.Filter = "Bitmap file (*.bmp)|*.bmp";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.SupportMultiDottedExtensions = false;
            saveFileDialog.Title = "Save Screen As";

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                screen.Save(saveFileDialog.FileName);
            }
        }

        private void LG_scrollCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			bool @checked = checkBox.Checked;
			this.LG_autoScroll = @checked;
			if (@checked)
			{
				this.LG_ScrollToEnd();
			}
		}

		private void LG_ScrollToEnd()
		{
			this.LG_logTextBox.SelectionStart = this.LG_logTextBox.Text.Length;
			this.LG_logTextBox.ScrollToCaret();
		}

		private void limitCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			short num = Convert.ToInt16(checkBox.Tag);
			bool @checked = checkBox.Checked;
			switch (num)
			{
				case 0:
					this.AISettings.AD_EnableLimit = @checked;
					return;

				case 1:
					this.AISettings.GC_EnableLimit = @checked;
					return;

				case 2:
					this.AISettings.AR_EnableLimit = @checked;
					return;

				case 3:
					this.AISettings.RD_EnableLimit = @checked;
					return;

				case 4:
					this.AISettings.RD_EnableDragonLimit = @checked;
					return;

				default:
					return;
			}
		}

		private void limitNumericBox_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = sender as NumericUpDown;
			short num = Convert.ToInt16(numericUpDown.Tag);
			int num2 = Convert.ToInt32(numericUpDown.Value);
			switch (num)
			{
				case 0:
					this.AISettings.AD_Limit = num2;
					return;

				case 1:
					this.AISettings.GC_Limit = num2;
					return;

				case 2:
					this.AISettings.AR_Limit = num2;
					return;

				case 3:
					this.AISettings.RD_Limit = num2;
					return;

				case 4:
					this.AISettings.RD_DragonLimit = num2;
					return;

				default:
					return;
			}
		}

		private void LoadWaveSkills(Panel[] wavePanels, int[][] waveSkill, int offset)
		{
			for (int i = 0; i < wavePanels.Length; i++)
			{
				if (waveSkill[i] != null)
				{
					int[] array = waveSkill[i];
					for (int j = 0; j < array.Length; j++)
					{
						int tag = array[j];
						Button item = this.FindControlByTag(wavePanels[i].Controls, tag) as Button;
						this._skillButtons[i + offset].Add(item);
					}
					this.SetSkills(i + offset);
				}
			}
		}

		private void LogPixel()
		{
			if (this.Worker != null)
			{
				this.AIProfiles.TMP_LogPixel = true;
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{ }

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.contactUsLinkLabel.Parent = this.topheaderPictureBox;
			this.contactUsLinkLabel.BackColor = Color.Transparent;
			ContextMenu contextMenu = new ContextMenu();
			MenuItem menuItem = new MenuItem("Pause");
			contextMenu.MenuItems.Add(menuItem);
			this.aiButton.ContextMenu = contextMenu;
			menuItem.Enabled = false;
			menuItem.Click += delegate (object _sender, EventArgs _e)
			{
				this.PauseAI();
			};

			//Loading Sound file and preparing it to play if needed.
			this.AlertSound = new SoundPlayer(SevenKnightsAI.Properties.Resources.Alien_AlarmDrum_KevanGC_893953959);
			string build = "v" + this.ProductVersion + "-" + Assembly.GetExecutingAssembly().GetLinkerTime().ToShortDateString();
			this.tsslBuildInfo.Text = "Build: " + build;
			AppendLog("Loaded Build: " + build);
			this.loaded = true;
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{ }

		private void masteryComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			if (comboBox == null)
			{
				return;
			}
			short num = Convert.ToInt16(comboBox.Tag);
			Mastery selectedIndex = (Mastery)comboBox.SelectedIndex;
			switch (num)
			{
				case 0:
					this.AISettings.AD_Mastery = selectedIndex;
					return;

				case 1:
					this.AISettings.GC_Mastery = selectedIndex;
					return;

				case 2:
					this.AISettings.AR_Mastery = selectedIndex;
					return;

				default:
					return;
			}
		}

		private void PauseAI()
		{
			if (this.Worker != null)
			{
				this.AIProfiles.TMP_Paused = true;
				this.aiButton.Text = "&Resume AI";
				this.EnablePause(false);
			}
		}

		private void ReloadTabs(bool refreshSettings)
		{
			this.InitAdventureTab();
			this.InitGlobalProfile();
			this.InitGoldChamberTab();
			this.InitArenaTab();
			this.InitRaidTab();
			this.InitResourcesTab();
			this.InitLogsTab();
			if (refreshSettings)
			{
				this.InitSettingsTab();
			}
		}

		private void ResumeAI()
		{
			this.AIProfiles.TMP_Paused = false;
			this.AIProfiles.TMP_WaitingForKeys = false;
			this.tsslCursorPosition.Text = "";
			this.aiButton.Text = "Stop AI";
			this.EnablePause(true);
		}

		private void RS_buyKeysCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			short num = Convert.ToInt16(checkBox.Tag);
			bool @checked = checkBox.Checked;
			if (num == 0)
			{
				this.AISettings.RS_BuyKeyHonors = @checked;
				return;
			}
			if (num == 1)
			{
				this.AISettings.RS_BuyKeyRubies = @checked;
			}
		}

		private void RS_buyKeysComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			short num = Convert.ToInt16(comboBox.Tag);
			int selectedIndex = comboBox.SelectedIndex;
			if (num == 0)
			{
				this.AISettings.RS_BuyKeyHonorsType = (BuyKeyHonorsType)selectedIndex;
				return;
			}
			if (num == 1)
			{
				this.AISettings.RS_BuyKeyRubiesType = (BuyKeyRubiesType)selectedIndex;
			}
		}

		private void RS_buyKeysNumericBox_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = sender as NumericUpDown;
			short num = Convert.ToInt16(numericUpDown.Tag);
			int num2 = Convert.ToInt32(numericUpDown.Value);
			if (num == 0)
			{
				this.AISettings.RS_BuyKeyHonorsAmount = num2;
				return;
			}
			if (num == 1)
			{
				this.AISettings.RS_BuyKeyRubiesAmount = num2;
			}
		}

		private void RS_collectGiftCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			short num = Convert.ToInt16(checkBox.Tag);
			bool @checked = checkBox.Checked;
			switch (num)
			{
				case 0:
					this.AISettings.RS_CollectLuckyChest = @checked;
					return;

				case 1:
					this.AISettings.RS_CollectLuckyBox = @checked;
					return;

				default:
					return;
			}
		}

		private void RS_collectInboxCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			short num = Convert.ToInt16(checkBox.Tag);
			bool @checked = checkBox.Checked;
			switch (num)
			{
				case 0:
					this.AISettings.RS_InboxHonors = @checked;
					return;

				case 1:
					this.AISettings.RS_InboxKeys = @checked;
					return;

				case 2:
					this.AISettings.RS_InboxGold = @checked;
					return;

				case 3:
					this.AISettings.RS_InboxRubies = @checked;
					return;

				case 4:
					this.AISettings.RS_InboxTickets = @checked;
					return;

				default:
					return;
			}
		}

		private void RS_collectQuestsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			short num = Convert.ToInt16(checkBox.Tag);
			bool @checked = checkBox.Checked;
			switch (num)
			{
				case 0:
					this.AISettings.RS_SpecialQuestsDaily = @checked;
					return;

				case 1:
					this.AISettings.RS_SpecialQuestsWeekly = @checked;
					return;

				case 2:
					this.AISettings.RS_SpecialQuestsMonthly = @checked;
					return;

				case 3:
					this.AISettings.RS_QuestsBattle = @checked;
					return;

				case 4:
					this.AISettings.RS_QuestsHero = @checked;
					return;

				case 5:
					this.AISettings.RS_QuestsItem = @checked;
					return;

				case 6:
					this.AISettings.RS_QuestsSocial = @checked;
					return;

				default:
					return;
			}
		}

		private void RS_sellAllRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			short num = Convert.ToInt16(radioButton.Tag);
			bool @checked = radioButton.Checked;
			if (num == 0)
			{
				this.RS_heroAmountNumericBox.Enabled = !@checked;
				this.AISettings.RS_SellHeroAll = @checked;
				return;
			}
			if (num == 1)
			{
				this.RS_itemAmountNumericBox.Enabled = !@checked;
				this.AISettings.RS_SellItemAll = @checked;
			}
		}

		private void RS_sellAmountNumericBox_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = sender as NumericUpDown;
			short num = Convert.ToInt16(numericUpDown.Tag);
			int num2 = Convert.ToInt32(numericUpDown.Value);
			if (num == 0)
			{
				this.AISettings.RS_SellHeroAmount = num2;
				return;
			}
			if (num == 1)
			{
				this.AISettings.RS_SellItemAmount = num2;
			}
		}

		private void RS_sellCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			short num = Convert.ToInt16(checkBox.Tag);
			bool @checked = checkBox.Checked;
			if (num == 0)
			{
				this.AISettings.RS_SellHeroes = @checked;
				return;
			}
			if (num == 1)
			{
				this.AISettings.RS_SellItems = @checked;
			}
		}

		private void RS_sellStarsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			short num = Convert.ToInt16(comboBox.Tag);
			int num2 = comboBox.SelectedIndex + 1;
			if (num == 0)
			{
				this.AISettings.RS_SellHeroStars = num2;
				return;
			}
			if (num == 1)
			{
				this.AISettings.RS_SellItemStars = num2;
			}
		}

		private void RS_sendHonorsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			short num = Convert.ToInt16(checkBox.Tag);
			bool @checked = checkBox.Checked;
			if (num == 0)
			{
				this.AISettings.RS_SendHonorsFacebook = @checked;
				return;
			}
			if (num == 1)
			{
				this.AISettings.RS_SendHonorsInGame = @checked;
			}
		}

		private void saveSettingsButton_Click(object sender, EventArgs e)
		{
			try
			{
				this.AIProfiles.Save();
				MessageBox.Show("Settings has been saved!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Cannot save settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void SetAutoSkill(int tag, int option)
		{
			SkillType skillType = SkillType.Both;
			if (option == 0)
			{
				skillType = SkillType.Auto;
			}
			else if (option == 1)
			{
				skillType = SkillType.Manual;
			}
			else if (option == 2)
			{
				skillType = SkillType.Both;
			}
			switch (tag)
			{
				case 0:
					this.AISettings.AD_SkillType = skillType;
					return;

				case 1:
					this.AISettings.GC_SkillType = skillType;
					return;

				case 2:
					this.AISettings.RD_SkillType = skillType;
					return;

				default:
					return;
			}
		}

		private void SetFormation(int tag, int index)
		{
			switch (tag)
			{
				case 0:
					this.AISettings.AD_Formation = (Formation)index;
					return;

				case 1:
					this.AISettings.GC_Formation = (Formation)index;
					return;

				default:
					return;
			}
		}

		private void SetFormationPosition(int tag, int index)
		{
			Point[][] formationPositions = this._formationPositions;
			int num = 0;
			CheckBox[] array = this._formationCheckBoxes[tag];
			for (int i = 0; i < array.Length; i++)
			{
				CheckBox checkBox = array[i];
				checkBox.Location = formationPositions[index][num++];
			}
		}

		private void SetFormationVisibility(int tag, bool visible)
		{
			CheckBox[] array = this._formationCheckBoxes[tag];
			for (int i = 0; i < array.Length; i++)
			{
				CheckBox checkBox = array[i];
				checkBox.Visible = visible;
			}
		}

		private void SetSkills(int parentTag)
		{
			List<Button> list = this._skillButtons[parentTag];
			int[] array = new int[list.Count];
			int num = 0;
			foreach (Button current in list)
			{
				current.Text = (num + 1).ToString();
				array[num] = (int)Convert.ToInt16(current.Tag);
				num++;
			}
			switch (parentTag)
			{
				case 0:
					this.AISettings.AD_Wave1Skills = array;
					return;

				case 1:
					this.AISettings.AD_Wave2Skills = array;
					return;

				case 2:
					this.AISettings.AD_Wave3Skills = array;
					return;

				case 3:
					this.AISettings.GC_Wave1Skills = array;
					return;

				case 4:
					this.AISettings.GC_Wave2Skills = array;
					return;

				case 5:
					this.AISettings.RD_Team1Skills = array;
					return;

				case 6:
					this.AISettings.RD_Team2Skills = array;
					return;

				default:
					return;
			}
		}

		private void skillButton_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			short num = Convert.ToInt16(button.Parent.Tag);
			List<Button> list = this._skillButtons[(int)num];
			if (list.Contains(button))
			{
				button.Text = string.Empty;
				list.Remove(button);
			}
			else
			{
				if (list.Count == 10)
				{
					list[0].Text = string.Empty;
					list.RemoveAt(0);
				}
				list.Add(button);
			}
			this.SetSkills((int)num);
		}

		private void skillLoopCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			short num = Convert.ToInt16(checkBox.Parent.Tag);
			bool @checked = checkBox.Checked;
			if (@checked)
			{
				checkBox.BackColor = this.COLOR_SKILL_LOOP;
			}
			else
			{
				checkBox.BackColor = Control.DefaultBackColor;
			}
			switch (num)
			{
				case 0:
					this.AISettings.AD_Wave1Loop = @checked;
					return;

				case 1:
					this.AISettings.AD_Wave2Loop = @checked;
					return;

				case 2:
					this.AISettings.AD_Wave3Loop = @checked;
					return;

				case 3:
					this.AISettings.GC_Wave1Loop = @checked;
					return;

				case 4:
					this.AISettings.GC_Wave2Loop = @checked;
					return;

				case 5:
					this.AISettings.RD_Team1Loop = @checked;
					return;

				case 6:
					this.AISettings.RD_Team2Loop = @checked;
					return;

				default:
					return;
			}
		}

		private void skillTypeRadio_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			short option = Convert.ToInt16(radioButton.Tag);
			short tag = Convert.ToInt16(radioButton.Parent.Tag);
			this.SetAutoSkill((int)tag, (int)option);
		}

		private void ST_currentProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			ProfileComboBoxItem profileComboBoxItem = comboBox.SelectedItem as ProfileComboBoxItem;
			this.AIProfiles.ChangeProfile(profileComboBoxItem.Key);
			this.ReloadTabs(false);
		}

		private void ST_delayTrackBar_ValueChanged(object sender, EventArgs e)
		{
			TrackBar trackBar = sender as TrackBar;
			int value = trackBar.Value;
			this.ST_delayValueLabel.Text = value.ToString() + " ms";
			this.AIProfiles.ST_Delay = value;
		}

		private void ST_forceActiveCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			bool @checked = checkBox.Checked;
			this.AIProfiles.ST_BlueStacksForceActive = @checked;
		}

		private void ST_foregroundMode_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			bool @checked = checkBox.Checked;
			this.ST_ToggleBlueStacks(true, true);
			this.AIProfiles.ST_ForegroundMode = @checked;
		}

		private void ST_hotTimeProfileCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			bool @checked = checkBox.Checked;
			this.AIProfiles.ST_EnableHotTimeProfile = @checked;
		}

		private void ST_hotTimeProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			ProfileComboBoxItem profileComboBoxItem = comboBox.SelectedItem as ProfileComboBoxItem;
			this.AIProfiles.ST_HotTimeProfile = profileComboBoxItem.Key;
		}

		private void ST_manageProfileButton_Click(object sender, EventArgs e)
		{ }

		private void ST_opacityTrackBar_ValueChanged(object sender, EventArgs e)
		{
			TrackBar trackBar = sender as TrackBar;
			int value = trackBar.Value;
			base.Opacity = (double)value / (double)trackBar.Maximum;
		}

		private void ST_pushbulletCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			bool @checked = checkBox.Checked;
			this.AIProfiles.ST_PushbulletEnable = @checked;
		}

		private void ST_pushbulletEmailTextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			string text = textBox.Text;
			this.AIProfiles.ST_PushbulletEmail = text;
		}

		private void ST_reconnectInterruptCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			bool @checked = checkBox.Checked;
			this.AIProfiles.ST_ReconnectInterruptEnable = @checked;
		}

		private void ST_reconnectNumericBox_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = sender as NumericUpDown;
			decimal value = numericUpDown.Value;
			this.AIProfiles.ST_ReconnectInterruptInterval = (int)value;
		}

		private void ST_RefreshProfiles()
		{
			int num = 0;
			this.ST_currentProfileComboBox.Items.Clear();
			this.ST_hotTimeProfileComboBox.Items.Clear();
			foreach (KeyValuePair<string, AISettings> current in this.AIProfiles.Settings)
			{
				ProfileComboBoxItem item = new ProfileComboBoxItem(current);
				this.ST_currentProfileComboBox.Items.Add(item);
				this.ST_hotTimeProfileComboBox.Items.Add(item);
				if (current.Key == this.AIProfiles.CurrentKey)
				{
					this.ST_currentProfileComboBox.SelectedIndex = num;
					if (string.IsNullOrEmpty(this.AIProfiles.ST_HotTimeProfile))
					{
						this.ST_hotTimeProfileComboBox.SelectedIndex = this.ST_currentProfileComboBox.SelectedIndex;
					}
				}
				if (current.Key == this.AIProfiles.ST_HotTimeProfile && this.ST_hotTimeProfileComboBox.SelectedIndex == -1)
				{
					this.ST_hotTimeProfileComboBox.SelectedIndex = num;
				}
				num++;
			}
		}

		private void ST_ToggleBlueStacks(bool force = false, bool show = true)
		{
			if (this.AI.BlueStacks == null || this.AI.BlueStacks.MainWindowAS == null || this.AI.BlueStacks.SideMenuAS == null)
			{
				return;
			}
			string arg;
			if (this.AI.BlueStacks.IsHidden || (force && show))
			{
				this.AI.BlueStacks.Show(true);
				arg = "Hide";
			}
			else
			{
				this.AI.BlueStacks.Hide(true);
				arg = "Show";
			}
			this.ST_toggleBlueStacksButton.Text = string.Format("{0} BlueStacks", arg);
		}

		private void ST_toggleBlueStacksButton_Click(object sender, EventArgs e)
		{
			this.ST_ToggleBlueStacks(false, true);
		}

		private void StartAI()
		{
			this.Worker = this.AI.Start(this.SynchronizationContext);
			this.Worker.ProgressChanged += new ProgressChangedEventHandler(this.BackgroundWorkerOnProgressChanged);
			this.Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackgroundWorkerOnCompleted);
			this.started = true;
			this.aiButton.Text = "Stop AI";
			this.EnablePause(true);
			this.LG_LogPixel.Enabled = true;
            this.LG_SaveScreen.Enabled = true;
			this.ST_toggleBlueStacksButton.Enabled = true;
		}

		private void StopAI()
		{
			if (this.Worker != null)
			{
				this.Worker.CancelAsync();
				this.EnablePause(false);
				this.LG_LogPixel.Enabled = false;
                this.LG_SaveScreen.Enabled = false;
				this.profileToolStripLabel.Text = string.Empty;
				this.AIProfiles.TMP_Paused = false;
				this.AIProfiles.TMP_WaitingForKeys = false;
				if (this.AIProfiles.TMP_UsingHotTimeProfile)
				{
					this.AIProfiles.ToggleHotTimeProfile();
					for (int i = 0; i < 2; i++)
					{
						this.ReloadTabs(true);
					}
				}
			}
		}

		private void teamComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			if (comboBox == null)
			{
				return;
			}
			short num = Convert.ToInt16(comboBox.Tag);
			Team selectedIndex = (Team)comboBox.SelectedIndex;
			switch (num)
			{
				case 0:
					this.AISettings.AD_Team = selectedIndex;
					return;

				case 1:
					this.AISettings.GC_Team = selectedIndex;
					return;

				default:
					return;
			}
		}

		private void UpdateCurrentProfileStatus()
		{
			this.profileToolStripLabel.Text = string.Format("[Profile: {0}]", this.AIProfiles.CurrentProfileName);
		}

        #endregion Private Methods

        private void AD_CheckingHeroes_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            this.AISettings.AD_CheckingHeroes = checkBox.Checked;
        }

	}
}