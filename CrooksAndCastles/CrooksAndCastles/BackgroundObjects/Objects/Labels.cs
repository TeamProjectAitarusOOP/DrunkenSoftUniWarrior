using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrunkenSoftUniWarrior.BackgroundObjects.Objects
{
    class Labels
    {
        public Labels(int x, int y, string text, int sizeX, int sizeY,int sizeText)
        {
            this.Label = new Label();
            this.Label.Location = new System.Drawing.Point(x, y);
            this.Label.Text = text;
            this.Label.Size = new System.Drawing.Size(sizeX, sizeY);
            this.Label.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", sizeText, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label.ForeColor = System.Drawing.Color.Black;
        }

        public Labels(int x, int y, int sizeX, int sizeY, int sizeText)
        {
            this.Label = new Label();
            this.Label.Location = new System.Drawing.Point(x, y);
            this.Label.Size = new System.Drawing.Size(sizeX, sizeY);
            this.Label.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", sizeText, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label.ForeColor = System.Drawing.Color.Black;
        }

        public Label Label { get; set; }

        public void SetText(string text)
        {
            Label.Text = text;
        }
    }
}
