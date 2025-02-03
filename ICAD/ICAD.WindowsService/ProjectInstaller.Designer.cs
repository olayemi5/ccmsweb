namespace ICAD.WindowsService
{
    partial class ProjectInstaller
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
            this.IcadserviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.IcadserviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // IcadserviceProcessInstaller
            // 
            this.IcadserviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.IcadserviceProcessInstaller.Password = null;
            this.IcadserviceProcessInstaller.Username = null;
            // 
            // IcadserviceInstaller
            // 
            this.IcadserviceInstaller.Description = "This service auto-push ICAD records to NIBSS ICAD Web service";
            this.IcadserviceInstaller.DisplayName = "ICAD Service";
            this.IcadserviceInstaller.ServiceName = "ICADService";
            this.IcadserviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.IcadserviceProcessInstaller,
            this.IcadserviceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller IcadserviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller IcadserviceInstaller;
    }
}