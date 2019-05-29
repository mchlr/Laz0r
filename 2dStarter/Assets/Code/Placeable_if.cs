using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Placeable_if
{

    void hover(bool b);
    bool add();
    void del();
    //void move();
    void setPos();
    Vector3 getPos();
    bool isObjMarked(Placeable_if otherObj);

}
