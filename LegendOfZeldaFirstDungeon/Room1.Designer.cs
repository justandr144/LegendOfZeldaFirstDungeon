
namespace LegendOfZeldaFirstDungeon
{
    partial class Room1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameLoop = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // gameLoop
            // 
            this.gameLoop.Enabled = true;
            this.gameLoop.Interval = 20;
            this.gameLoop.Tick += new System.EventHandler(this.gameLoop_Tick);
            // 
            // Room1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LegendOfZeldaFirstDungeon.Properties.Resources.FirstRoom;
            this.DoubleBuffered = true;
            this.Name = "Room1";
            this.Size = new System.Drawing.Size(870, 700);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Room1_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Room1_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Room1_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer gameLoop;
    }
}
