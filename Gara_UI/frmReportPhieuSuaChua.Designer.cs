﻿namespace Gara_UI
{
    partial class frmReportPhieuSuaChua
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.PhieuSuaChuaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new Gara_UI.DataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PhieuSuaChuaTableAdapter = new Gara_UI.DataSet1TableAdapters.PhieuSuaChuaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuSuaChuaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // PhieuSuaChuaBindingSource
            // 
            this.PhieuSuaChuaBindingSource.DataMember = "PhieuSuaChua";
            this.PhieuSuaChuaBindingSource.DataSource = this.DataSet1;
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "DataSet1";
            this.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapWidth = 35;
            reportDataSource1.Name = "DataSet2";
            reportDataSource1.Value = this.PhieuSuaChuaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Gara_UI.Report3.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(578, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // PhieuSuaChuaTableAdapter
            // 
            this.PhieuSuaChuaTableAdapter.ClearBeforeFill = true;
            // 
            // frmReportPhieuSuaChua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 450);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmReportPhieuSuaChua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu Sửa Chữa";
            this.Load += new System.EventHandler(this.frmReportPhieuSuaChua_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PhieuSuaChuaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PhieuSuaChuaBindingSource;
        private DataSet1 DataSet1;
        private DataSet1TableAdapters.PhieuSuaChuaTableAdapter PhieuSuaChuaTableAdapter;
    }
}