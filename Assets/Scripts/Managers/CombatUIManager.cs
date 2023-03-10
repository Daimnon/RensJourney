using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CombatUIManager : MonoBehaviour
{
    private static CombatUIManager _instance;
    public static CombatUIManager Instance => _instance;

    [SerializeField] private CombatSkillMenu _combatSkillMenu;

    public CombatSkillMenu CombatSkillMenu => _combatSkillMenu;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void RefreshCombatSkillMenuDisplay(Player invokingP, Character lastCharacterClickedOn, Vector2 newPosUI, Skill[] activeSkills, int lvl)
    {
        _combatSkillMenu.transform.position = newPosUI;

        for (int i = 0; i < _combatSkillMenu.Skills.Length; i++)
        {
            if (!activeSkills[i])
            {
                _combatSkillMenu.Skills[i].interactable = false;
                continue;
            }

            _combatSkillMenu.Skills[i].interactable = true;

            //activeSkills[i].InvokerC = invokingP;
            _combatSkillMenu.Skills[i].onClick.RemoveAllListeners();
            _combatSkillMenu.Skills[i].onClick.AddListener(invokingP.ActivateSkill);
            Debug.Log($"Populated {_combatSkillMenu.Skills[i].name} successfuly");
        }

        _combatSkillMenu.LevelTMProUGUI.text = $"{_combatSkillMenu.LevelText} {lvl}";

        Color color;
        if (lastCharacterClickedOn is Enemy)
        {
            color = new Color(255f, 0f, 0f, 0.47f);
            _combatSkillMenu.CenterImage.color = color;
        }
        else if (lastCharacterClickedOn is Ally)
        {
            color = new Color(0f, 255f, 0f, 0.47f);
            _combatSkillMenu.CenterImage.color = color;
        }
        else
        {
            color = new Color(255f, 255f, 255f, 0.47f);
            _combatSkillMenu.CenterImage.color = color;
        }
    }
}
