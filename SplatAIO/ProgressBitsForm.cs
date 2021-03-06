﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplatAIO
{
    public partial class ProgressBitsForm : Form
    {
        public static readonly uint progressBitsAddress = 0x12CD1C24;
        private TCPGecko gecko;
        private uint progression;

        public ProgressBitsForm()
        {
            InitializeComponent();
        }

        private void ProgressBitsForm_Load(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.Owner;
            gecko = mainForm.Gecko;
            progression = gecko.peek(progressBitsAddress + mainForm.diff);

            tutorialBox.Checked = (progression & 0x1) != 0;
            splatfestBox.Checked = (progression & 0x2) != 0;
            rankedNewsBox.Checked = ((progression & 0x4) != 0);
            lobbyBox.Checked = (progression & 0x8) != 0;
            heroSuitBox.Checked = (progression & 0x10) != 0;
            greatZapfishBox.Checked = (progression & 0x80) != 0;
            cuttlefishPostGameBox.Checked = (progression & 0x100) != 0;
            rankedUnlockedBox.Checked = (progression & 0x800) != 0;
            rankShownBox.Checked = (progression & 0x1000) != 0;
            snailsShownBox.Checked = (progression & 0x10000) != 0;
            levelCapRaisedBox.Checked = (progression & 0x100000) != 0;
            warningBox.Checked = (progression & 0x200000) != 0;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            SetFlag(ref progression, 0x1, tutorialBox.Checked);
            SetFlag(ref progression, 0x2, splatfestBox.Checked);
            SetFlag(ref progression, 0x4, rankedNewsBox.Checked);
            SetFlag(ref progression, 0x8, lobbyBox.Checked);
            SetFlag(ref progression, 0x10, heroSuitBox.Checked);
            SetFlag(ref progression, 0x80, greatZapfishBox.Checked);
            SetFlag(ref progression, 0x100, cuttlefishPostGameBox.Checked);
            SetFlag(ref progression, 0x800, rankedUnlockedBox.Checked);
            SetFlag(ref progression, 0x1000, rankShownBox.Checked);
            SetFlag(ref progression, 0x10000, snailsShownBox.Checked);
            SetFlag(ref progression, 0x100000, levelCapRaisedBox.Checked);
            SetFlag(ref progression, 0x200000, warningBox.Checked);

            gecko.poke32(progressBitsAddress + ((Form1)this.Owner).diff, progression);
        }

        public static void SetFlag(ref uint progression, uint flag, bool checkbox)
        {
            if (((progression & flag) != 0) && !checkbox)
            {
                // the flag is set, but we don't want it to be, so remove the flag
                progression ^= flag;
            }
            else if (((progression & flag) == 0) && checkbox)
            {
                // the flag isn't set, but we need it to be, so set the flag
                progression |= flag;
            }
        }

    }
}
