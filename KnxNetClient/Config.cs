using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Knx
{

    public class ConfigIO
    {

        internal static ConfigList read(string XmlFileName)
        {
            ConfigList list;
            FileStream fs = null;
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(ConfigList));
                fs = new FileStream(XmlFileName, FileMode.Open);
                if (fs == null) return null;
                XmlReader reader = new XmlTextReader(fs);
                list = (ConfigList)mySerializer.Deserialize(reader);

                return list;
            }
            catch (FileNotFoundException fnfeX)
            {
                //list = null;
                //Debug(fnfeX.ToString());
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }


        }

    }



    #region aus XML generiert


    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ConfigList
    {

        private string defaultConfigField;

        private ConfigListConfig[] configField;

        /// <remarks/>
        public string defaultConfig
        {
            get
            {
                return this.defaultConfigField;
            }
            set
            {
                this.defaultConfigField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Config")]
        public ConfigListConfig[] Config
        {
            get
            {
                return this.configField;
            }
            set
            {
                this.configField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigListConfig
    {

        private string nameField;

        private ConfigListConfigLight[] lightListField;

        private ConfigListConfigRollo[] rolloListField;

        private ConfigListConfigLightHell[] lightHellListField;


        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Light", IsNullable = false)]
        public ConfigListConfigLight[] LightList
        {
            get
            {
                return this.lightListField;
            }
            set
            {
                this.lightListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Light", IsNullable = false)]
        public ConfigListConfigLightHell[] LightHellList
        {
            get
            {
                return this.lightHellListField;
            }
            set
            {
                this.lightHellListField = value;
            }
        }


        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Rollo", IsNullable = false)]
        public ConfigListConfigRollo[] RolloList
        {
            get
            {
                return this.rolloListField;
            }
            set
            {
                this.rolloListField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigListConfigLight
    {

        private string nameField;

        private string eibAdress_IOField;

        private string eibAdress_DimmField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string EibAdress_IO
        {
            get
            {
                return this.eibAdress_IOField;
            }
            set
            {
                this.eibAdress_IOField = value;
            }
        }

        /// <remarks/>
        public string EibAdress_Dimm
        {
            get
            {
                return this.eibAdress_DimmField;
            }
            set
            {
                this.eibAdress_DimmField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigListConfigLightHell
    {

        private string nameField;

        private string eibAdress_HellField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string EibAdress_Hell
        {
            get
            {
                return this.eibAdress_HellField;
            }
            set
            {
                this.eibAdress_HellField = value;
            }
        }

    }


    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigListConfigRollo
    {

        private string nameField;

        private string eibAdress_AufAbField;

        private string eibAdress_LamelleField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string EibAdress_AufAb
        {
            get
            {
                return this.eibAdress_AufAbField;
            }
            set
            {
                this.eibAdress_AufAbField = value;
            }
        }

        /// <remarks/>
        public string EibAdress_Lamelle
        {
            get
            {
                return this.eibAdress_LamelleField;
            }
            set
            {
                this.eibAdress_LamelleField = value;
            }
        }
    }



    #endregion

    

}