using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mst.Main.CheatCoded
{
    public class CheatCoderComponent : MonoBehaviour
    {
        public CheatCoderData[] cheatCodeList;
        private GameObject _inputObject;
        private Button _showButton; 
        private Button _executeButton;
        private InputField _cheatInpuField;
        private CheatCoderData _currentCheatCoderData; 

        void OnEnable(){
            _inputObject = transform.Find("InputObject").gameObject;
            if(_inputObject.activeInHierarchy)
                _inputObject.SetActive(false);

            _showButton = transform.Find("ShowButtonObject").GetComponent<Button>();
            _showButton.onClick.AddListener(ShowInputObject);

            _executeButton = _inputObject.transform.Find("ExecuteButtonObject").GetComponent<Button>();
            _executeButton.onClick.AddListener(ExecuteCheat);

            _cheatInpuField = _inputObject.transform.Find("InputFieldObject").GetComponent<InputField>();
            _cheatInpuField.onValueChanged.AddListener(CheatInputChanged);

            AdjustButtons(_cheatInpuField.text);
        }


        private void ShowInputObject(){
            _inputObject.SetActive(!_inputObject.activeInHierarchy);
            AdjustButtons(_cheatInpuField.text);
        }

        private void ExecuteCheat(){
            if(_currentCheatCoderData != null)
            {
                _currentCheatCoderData.cheatEvent.Invoke();
                _cheatInpuField.text = "";
                _inputObject.SetActive(false);
            }
        }

        private void CheatInputChanged(string str){
            AdjustButtons(str);
        }

        private void AdjustButtons(string cheatCode){
            if(cheatCode.Length > 0)
            {
                _currentCheatCoderData = GetCheatCoderData(cheatCode);
                if(_currentCheatCoderData != null)
                    _executeButton.interactable = true;
                else
                    _executeButton.interactable = false;
            }
            else
            {
                _executeButton.interactable = false;
            }
        }

        private CheatCoderData GetCheatCoderData(string cheatCode)
        {
            string normalizedInputCode = cheatCode.ToLower().Trim();
            string normalizedCheatCode = "";

            foreach(CheatCoderData cheatCodeData in cheatCodeList)
            {
                normalizedCheatCode = cheatCodeData.cheatCode.ToLower().Trim();
                if(normalizedInputCode.Equals(normalizedCheatCode))
                    return cheatCodeData;
            }

            return null;
        }

        void Update(){
            if( Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) )
                ExecuteCheat();
        }


        void OnDisable(){
            _showButton.onClick.RemoveListener(ShowInputObject);
            _executeButton.onClick.RemoveListener(ExecuteCheat);
            _cheatInpuField.onValueChanged.RemoveListener(CheatInputChanged);
        }
    }

    [Serializable]
    public class CheatCoderData{
        public string cheatCode;
        public UnityEvent cheatEvent;
    }
}