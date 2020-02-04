using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class ActionManager : MonoBehaviour
    {
        public List<Action> actionSlots = new List<Action>();//list to hold actions shown in the inspector

        StateManager states;

        

        public void Init(StateManager st)
        {
            states = st;

            UpdateActionsOneHanded();
            UpdateActionsTwoHanded();
        }

        public void UpdateActionsOneHanded()
        {
            //SlotToEmpty();
            Weapon w = states.weaponManager.currentWeapon;

            for (int i = 0; i < w.actions.Count; i++)
            {
                Action a = GetAction(w.actions[i].input);                
                a.targetAnim = w.actions[i].targetAnim;
                
            }
        }

        public void UpdateActionsTwoHanded()
        {
            //SlotToEmpty();
            Weapon w = states.weaponManager.currentWeapon;

            for (int i = 0; i < w.twoHandedActions.Count; i++)
            {
                Action a = GetAction(w.twoHandedActions[i].input);
                a.targetAnim = w.twoHandedActions[i].targetAnim;
                a.type = w.twoHandedActions[i].type;
            }
        }

        void SlotToEmpty()
        {
            for (int i = 0; i < 3; i++)
            {
                Action a = GetAction((ActionInput)i);
                a.targetAnim = null;
                a.type = ActionTypes.attack;
            }
        }

        ActionManager()
        {
            for (int i = 0; i < 3; i++)
            {
                Action a = new Action();
                a.input = (ActionInput)i;
                actionSlots.Add(a);
            }
        }

        public Action GetActionSlot (StateManager st)
        {
            ActionInput a_input = GetActionInput(st);
            return GetAction(a_input);
        }

        Action GetAction(ActionInput inp)
        {
            
            for(int i = 0; i < actionSlots.Count; i++)
            {
                if(actionSlots[i].input == inp)
                {
                    return actionSlots[i];
                }                           
            }
            return null;
        }

        public ActionInput GetActionInput(StateManager st)//gets the inputs from the statemanager
        {
            

            if (st.rb)
            {
                return ActionInput.rb;
            }
            if (st.lb)
            {
                return ActionInput.lb;
            }
            if (st.x)
            {
                return ActionInput.x;
            }

            return ActionInput.rb;
        }
    }

    public enum ActionInput
    {
        x,rb,lb
    }

    public enum ActionTypes
    {
        attack, block
    }

    [System.Serializable]
    public class Action
    {
        public ActionInput input;
        public ActionTypes type;
        public string targetAnim;
    }
}