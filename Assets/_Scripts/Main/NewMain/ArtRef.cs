using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewRef", menuName = "Data/Art", order = 0)]
public class ArtRef : ScriptableObject
{
    public Color customCardColour;
    public Color adventureTip,characterTip,entranceTip,combatTip,challengeTip,setbackTip,climaxTip, twistTip;
    public Sprite locationSTip, goalSTip,villianSTip,enemySTip,raceSTip, backgroundSTip,classSTip, driveSTip,statsSTip,entranceSTip,combatSTip,challengeSTip,setbackSTip,climaxSTip, twistSTip,lockImg,unlockImg;

}
