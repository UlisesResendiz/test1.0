var soundFile:AudioClip;
 
function OnTriggerStay(trigger:Collider) {
    if(trigger.GetComponent.<Collider>().tag=="Player") {
        GetComponent.<AudioSource>().clip = soundFile;
        GetComponent.<AudioSource>().Play();
    }

    else{
        
        GetComponent.<AudioSource>().Stop();
    }
}