﻿using System.Management.Automation;
using PowerForensics.Ntfs;

namespace PowerForensics.Cmdlets
{
    #region GetAttrDefCommand

    /// <summary> 
    /// This class implements the Get-AttrDef cmdlet. 
    /// </summary> 
    [Cmdlet(VerbsCommon.Get, "AttrDef", DefaultParameterSetName = "ByVolume")]
    public class GetAttrDefCommand : PSCmdlet
    {
        #region Parameters

        /// <summary> 
        /// This parameter provides the Volume Name for the 
        /// AttrDef objects that will be returned.
        /// </summary> 
        [Parameter(Position = 0, ParameterSetName = "ByVolume")]
        public string VolumeName
        {
            get { return volume; }
            set { volume = value; }
        }
        private string volume;

        /// <summary> 
        /// 
        /// </summary> 
        [Alias("FullName")]
        [Parameter(Mandatory = true, ParameterSetName = "ByPath", ValueFromPipelineByPropertyName = true)]
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        private string path;

        #endregion Parameters

        #region Cmdlet Overrides

        /// <summary> 
        /// 
        /// </summary> 
        protected override void BeginProcessing()
        {
            Util.checkAdmin();
            
            if (ParameterSetName == "ByVolume")
            {
                Util.getVolumeName(ref volume);
            }
        }

        /// <summary> 
        /// The ProcessRecord method calls AttrDef.GetInstances() 
        /// method to iterate through each AttrDef object on the specified volume.
        /// </summary> 
        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case "ByVolume":
                    Util.getVolumeName(ref volume);
                    WriteObject(AttrDef.GetInstances(volume));
                    break;
                case "ByPath":
                    WriteObject(AttrDef.GetInstancesByPath(path));
                    break;
            }
        }
        
        #endregion Cmdlet Overrides
    }

    #endregion GetAttrDefCommand
}
