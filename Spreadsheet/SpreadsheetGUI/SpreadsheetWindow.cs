﻿using System;
using System.Windows.Forms;

namespace SSGui
{
    public partial class SpreadsheetWindow : Form, ISpreadsheetView
    {
        public SpreadsheetWindow()
        {
            InitializeComponent();
            ssp = spreadsheetPanel;
            ssp.SelectionChanged += SelectionChangedEvent;
            CellName = "A1";
        }

        /// <summary>
        /// Fire our selection event
        /// </summary>
        /// <param name="sender"></param>
        private void SelectionChangedEvent(SpreadsheetPanel sender)
        {
            if(SelectionEvent != null)
            {
                SelectionEvent(sender);
            }
        }

        /// <summary>
        /// The spreadsheetpanel
        /// </summary>
        private SpreadsheetPanel ssp;
        /// <summary>
        /// Fired when close button is clicked
        /// </summary>
        public event Action CloseEvent;
        /// <summary>
        /// Fired when a file is chosen with a file dialog
        /// The parameter is the chosen filename;
        /// </summary>
        public event Action<string> FileChosenEvent;
        /// <summary>
        /// Fired when a file is saved with a file dialog
        /// The parameter is the chosen filename;
        /// </summary>
        public event Action<string> FileSaveEvent;
        /// <summary>
        /// Fired when new button is clicked
        /// </summary>
        public event Action NewEvent;
        /// <summary>
        /// Fires when the contents box text is changed
        /// </summary>
        public event Action ContentsEvent;
        /// <summary>
        /// Fires when the red X is hit
        /// </summary>
        public event Action<FormClosingEventArgs> XCloseEvent;
        /// <summary>
        /// Fires when help is clicked
        /// </summary>
        public event Action HelpEvent;
        /// <summary>
        /// Handles when cell is selected
        /// </summary>
        public event Action<SpreadsheetPanel> SelectionEvent;

        /// <summary>
        /// Set the contents textbox
        /// </summary>
        public string Contents
        {
            set
            {
                contentsTextBox.Text = value;
            }
            get
            {
                return contentsTextBox.Text;
            }
        }

        /// <summary>
        /// Set the title
        /// </summary>
        public string Title
        {
            set
            {
                Text = value;
            }
        }

        /// <summary>
        /// Set the value textbox
        /// </summary>
        public string Value
        {
            set
            {
                valueTextBox.Text = value;
            }
        }

        /// <summary>
        /// Set the messagebox
        /// </summary>
        public string Message
        {
            set
            {
                MessageBox.Show(value);
            }
        }

        /// <summary>
        /// Set the Close warning box
        /// </summary>
        public DialogResult CloseMessage(string s)
        {
            return MessageBox.Show(null, s, "Close File?", MessageBoxButtons.YesNo);
        }

        /// <summary>
        /// Gets the spreadsheetpanel
        /// </summary>
        public SpreadsheetPanel spreadSheetPanel
        {
            get
            {
                return ssp;
            }
        }

        /// <summary>
        /// Set the name textbox
        /// </summary>
        public string CellName
        {
            set
            {
                cellNameTextBox.Text = value;
            }
        }

        /// <summary>
        /// Closes this window
        /// </summary>
        public void DoClose()
        {
            Close();
        }

        /// <summary>
        /// Opens new window
        /// </summary>
        public void OpenNew()
        {
            SpreadsheetContext.GetContext().RunNew();
        }

        /// <summary>
        /// Handles the click event of open button
        /// </summary>
        private void openFile_Click(object sender, EventArgs e)
        {
            openFileDialog.DefaultExt = "ss";
            openFileDialog.Filter = "Spreadsheet files(*.ss)|*.ss| All files |*.*";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                if (FileChosenEvent != null)
                {
                    FileChosenEvent(openFileDialog.FileName);
                }
            }
        }

        /// <summary>
        /// Handles the click event of the Close button
        /// </summary>
        private void closeItem_Click(object sender, EventArgs e)
        {
            if (CloseEvent != null)
            {
                CloseEvent();
            }
        }

        /// <summary>
        /// Handles the click event of the new window button
        /// </summary>
        private void newWindow_Click(object sender, EventArgs e)
        {
            if (NewEvent != null)
            {
                NewEvent();
            }
        }

        /// <summary>
        /// Handles the click event of the save button
        /// </summary>
        private void saveFile_Click(object sender, EventArgs e)
        {
            saveFileDialog.DefaultExt = "ss";
            saveFileDialog.Filter = "Spreadsheet files(*.ss)|*.ss| All files |*.*";
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                if (FileSaveEvent != null)
                {
                    FileSaveEvent(saveFileDialog.FileName);
                }
            }
        }

        /// <summary>
        /// Handles when the contents textbox is changed
        /// </summary>
        private void contentsTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ContentsEvent != null)
            {
                ContentsEvent();
            }
        }

        /// <summary>
        /// Handles when the user clicks the red X
        /// </summary>
        private void SpreadsheetWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XCloseEvent != null)
            {
                XCloseEvent(e);
            }
        }

        /// <summary>
        /// Handles when help is clicked
        /// </summary>
        private void helpTool_Click(object sender, EventArgs e)
        {
            if (HelpEvent != null)
            {
                HelpEvent();
            }
        }
    }
}
