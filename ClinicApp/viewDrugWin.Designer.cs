using System.ComponentModel;
using Telerik.WinControls.UI;

namespace ClinicApp
{
    partial class viewDrugWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.DrugView = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DrugView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrugView.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // DrugView
            // 
            this.DrugView.AutoScroll = true;
            this.DrugView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrugView.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrugView.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.DrugView.MasterTemplate.AllowDeleteRow = false;
            this.DrugView.MasterTemplate.AllowEditRow = false;
            this.DrugView.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.DrugView.MasterTemplate.EnableAlternatingRowColor = true;
            this.DrugView.MasterTemplate.EnableFiltering = true;
            this.DrugView.MasterTemplate.EnableGrouping = false;
            this.DrugView.MasterTemplate.EnablePaging = true;
            this.DrugView.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.DrugView.Name = "DrugView";
            this.DrugView.ReadOnly = true;
            this.DrugView.Size = new System.Drawing.Size(1179, 648);
            this.DrugView.TabIndex = 0;
            this.DrugView.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.DrugView_CellDoubleClick);
            this.DrugView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DrugView_MouseDoubleClick);
            // 
            // viewDrugWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 648);
            this.Controls.Add(this.DrugView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "viewDrugWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Available Drugs";
            this.Load += new System.EventHandler(this.viewDrugWin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DrugView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrugView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RadGridView DrugView;
    }
}