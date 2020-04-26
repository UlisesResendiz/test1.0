var Camera1 : Camera;

var Camera2 : Camera;

function OnTriggerEnter(){

    Camera1.enabled = false;

    Camera2.enabled = true;

}

function OnTriggerExit(){

    Camera1.enabled = true;

    Camera2.enabled = false;

}