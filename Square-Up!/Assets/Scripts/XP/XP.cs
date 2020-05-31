using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class will assign experience to the player upon an enemy falling in combat
XP will be % based*/

public class XP : MonoBehaviour {

    private int currentLevel = 1;
    private int skillPoints = 0;
    private int currentXP = 0;
    private int remainingXP = 0;
    private int enemiesKilled = 0;

    private const int TO_NEXT_LEVEL = 100;
    private const int TYPE_SMOL_XP_GAIN = 5;
    private const int TYPE_QUEST_XP_GAIN = 25;
    private const int TYPE_BIG_XP_GAIN = 100;

    void assignXP(int xpType, bool enemyKilled) {
        currentXP += xpType;

        if (enemyKilled) /* The enemy kill count won't increase if the player completes a quest, because they hadn't killed an enemy*/
            enemiesKilled++;
        
        if (currentXP >= TO_NEXT_LEVEL) { /*level up*/

            if (currentXP == TO_NEXT_LEVEL) { //apply level up, and reset current XP to 0. Nothing else is needed
                levelUp();
                currentXP = 0;
                return;
            }

            /*Add remaining XP to next level*/
            if (currentXP > TO_NEXT_LEVEL) {
                remainingXP = currentXP - TO_NEXT_LEVEL; // trim the excess XP and store it in a variable
                levelUp();
                remainingXP = currentXP; // the trimmed XP, after levelling up, is now the current xp
                remainingXP = 0; // reset this. I don't want its value messing anything up
            }
        }

    }

    void levelUp() { // increase level and apply skill points for new abilities
        currentLevel += 1;
        skillPoints += 1;
    }
    
}
