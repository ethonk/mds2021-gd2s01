
public class EquipmentSlot : ItemSlot                           //Extend from ItemSlot
{
    public EquipmentType EquipmentType;

    protected override void OnValidate()                        //Protected override to keep the image override as u equip (Automatic image assignment)
    {
        base.OnValidate();                                      //Calling base class's in onvalidate 
        gameObject.name = EquipmentType.ToString() + " Slot";   //Name object using the value of equipment type
            
    }

}
