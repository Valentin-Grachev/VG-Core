using System;
using System.Collections.Generic;
using VG.Internal;
using UnityEngine;

namespace VG
{
    public class Saves : Manager
    {
        public static event Action onDeleted;
        private static bool reseting = false;


        #region Internal

        private static class Data
        {
            private const string dictionarySeparator = "\n";
            private const string valuesSeparator = " | ";
            private const string pairSeparator = ": ";



            public static Dictionary<string, int> Int = new Dictionary<string, int>();
            public static Dictionary<string, float> Float = new Dictionary<string, float>();
            public static Dictionary<string, string> String = new Dictionary<string, string>();
            public static Dictionary<string, bool> Bool = new Dictionary<string, bool>();

            public static string Get()
            {
                string result = string.Empty;

                foreach (var item in Int)
                    result += item.Key.ToString() + pairSeparator + item.Value.ToString() + valuesSeparator;
                result += dictionarySeparator;

                foreach (var item in Float)
                    result += item.Key.ToString() + pairSeparator + item.Value.ToString() + valuesSeparator;
                result += dictionarySeparator;

                foreach (var item in String)
                    result += item.Key.ToString() + pairSeparator + item.Value.ToString() + valuesSeparator;
                result += dictionarySeparator;

                foreach (var item in Bool)
                    result += item.Key.ToString() + pairSeparator + item.Value.ToString() + valuesSeparator;

                return result;
            }

            public static void Load(string data)
            {
                Int.Clear(); Float.Clear(); String.Clear(); Bool.Clear();
                if (data == string.Empty) return;

                string[] dictionariesData = data.Split(dictionarySeparator);

                for (int i = 0; i < 4; i++)
                {
                    string[] valuesData = dictionariesData[i].Split(valuesSeparator);

                    if (i == 0) // Int
                        foreach (var valueData in valuesData)
                        {
                            if (valueData == string.Empty) break;
                            string[] pairData = valueData.Split(pairSeparator);
                            Int.Add(pairData[0], Convert.ToInt32(pairData[1]));
                        }

                    if (i == 1) // Float
                        foreach (var valueData in valuesData)
                        {
                            if (valueData == string.Empty) break;
                            string[] pairData = valueData.Split(pairSeparator);
                            Float.Add(pairData[0], Convert.ToSingle(pairData[1]));
                        }

                    if (i == 2) // String
                        foreach (var valueData in valuesData)
                        {
                            if (valueData == string.Empty) break;
                            string[] pairData = valueData.Split(pairSeparator);
                            String.Add(pairData[0], pairData[1]);
                        }

                    if (i == 3) // Bool
                        foreach (var valueData in valuesData)
                        {
                            if (valueData == string.Empty) break;
                            string[] pairData = valueData.Split(pairSeparator);
                            Bool.Add(pairData[0], Convert.ToBoolean(pairData[1]));
                        }
                }
                

            }




        }

        


        public class ItemInt
        {
            public event Action onChanged;
            private string _saveKey;

            public int Value
            {
                get => Data.Int[_saveKey];
                set
                {
                    Data.Int[_saveKey] = value;
                    onChanged?.Invoke();
                }
            }

            public ItemInt(string saveKey, int startValue)
            {
                _saveKey = saveKey;

                if (reseting) Value = startValue;
                else
                {
                    if (!Data.Int.ContainsKey(_saveKey)) Data.Int.Add(_saveKey, startValue);
                    Int.Add(saveKey, this);
                }  
            }
        }

        public class ItemFloat
        {
            public event Action onChanged;
            private string _saveKey;

            public float Value
            {
                get => Data.Float[_saveKey];
                set
                {
                    Data.Float[_saveKey] = value;
                    onChanged?.Invoke();
                }
            }

            public ItemFloat(string saveKey, float startValue)
            {
                _saveKey = saveKey;

                if (reseting) Value = startValue;
                else
                {
                    if (!Data.Float.ContainsKey(_saveKey)) Data.Float.Add(_saveKey, startValue);
                    Float.Add(saveKey, this);
                }
            }

        }

        public class ItemString
        {
            public event Action onChanged;
            private string _saveKey;

            public string Value
            {
                get => Data.String[_saveKey];
                set
                {
                    Data.String[_saveKey] = value;
                    onChanged?.Invoke();
                }
            }

            public ItemString(string saveKey, string startValue)
            {
                _saveKey = saveKey;

                if (reseting) Value = startValue;
                else
                {
                    if (!Data.String.ContainsKey(_saveKey)) Data.String.Add(_saveKey, startValue);
                    String.Add(saveKey, this);
                }
                
            }

        }

        public class ItemBool
        {
            public event Action onChanged;
            private string _saveKey;

            public bool Value
            {
                get => Data.Bool[_saveKey];
                set
                {
                    Data.Bool[_saveKey] = value;
                    onChanged?.Invoke();
                }
            }

            public ItemBool(string saveKey, bool startValue)
            {
                _saveKey = saveKey;

                if (reseting) Value = startValue;
                else
                {
                    if (!Data.Bool.ContainsKey(_saveKey)) Data.Bool.Add(_saveKey, startValue);
                    Bool.Add(saveKey, this);
                }
            }

        }

        #endregion

        [SerializeField] private StartSaveValues _startValues;


        public delegate void OnCommited(bool success);
        public static event OnCommited onCommited;

        public static Dictionary<string, ItemInt> Int = new Dictionary<string, ItemInt>();
        public static Dictionary<string, ItemFloat> Float = new Dictionary<string, ItemFloat>();
        public static Dictionary<string, ItemString> String = new Dictionary<string, ItemString>();
        public static Dictionary<string, ItemBool> Bool = new Dictionary<string, ItemBool>();

        private static Saves instance;

        public static bool Initialized => instance != null && service.initialized;

        private static SaveService service => instance.supportedService as SaveService;

        protected override string managerName => "VG Saves";


        protected override void OnInitialized()
        {
            instance = this;

            string savesData = service.GetData();
            Log(Core.Message.Initialized(managerName) + " Data: \n" + savesData);
            Data.Load(savesData);

            SaveCreator.Create(_startValues);
        }




        public static void Commit(OnCommited onCommited = null)
        {
            service.Commit(Data.Get(), (success) =>
            {
                onCommited?.Invoke(success);
                Saves.onCommited?.Invoke(success);

                if (success) instance.Log("Saves commited.");
                else instance.Log("Saves not commited.");
            });
        }



        public static void Delete()
        {
            reseting = true;
            SaveCreator.Create(instance._startValues);
            reseting = false;

            Commit((success) => 
            {
                if (success) instance.Log("Saves deleted.");
            });

            onDeleted?.Invoke();
        }


        
    }
}




