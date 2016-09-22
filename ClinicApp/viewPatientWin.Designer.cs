using System.ComponentModel;
using Telerik.WinControls.UI;

namespace ClinicApp
{
    partial class viewPatientWin
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
            this.PatientView = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.PatientView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientView.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // PatientView
            // 
            this.PatientView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PatientView.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PatientView.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.PatientView.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.PatientView.MasterTemplate.EnableFiltering = true;
            this.PatientView.MasterTemplate.EnablePaging = true;
            this.PatientView.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.PatientView.Name = "PatientView";
            this.PatientView.ReadOnly = true;
            this.PatientView.ShowGroupPanel = false;
            this.PatientView.Size = new System.Drawing.Size(1163, 609);
            this.PatientView.TabIndex = 0;
            this.PatientView.Text = "radGridView1";
            this.PatientView.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.PatientView_CellDoubleClick);
            this.PatientView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PatientView_MouseDoubleClick);
            // 
            // viewPatientWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 609);
            this.Controls.Add(this.PatientView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "viewPatientWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "viewPatientWin";
            this.Load += new System.EventHandler(this.viewPatientWin_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.viewPatientWin_MouseDoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.PatientView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RadGridView PatientView;
    }
}