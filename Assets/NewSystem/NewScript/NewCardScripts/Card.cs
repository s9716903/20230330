public class Card
{
    public int ID;
    public Card(int _id)
    {
        this.ID = _id;
    }
}
public class TwinCard :Card
{
    public int Type1; //Type1(0:Move/1:PhyATK/2:MagATK/3:Star/4:Health)
    public int Value1; //Type1 Value
    public int[] AttackZone1;
    public int Type2; //Type2(0:Move/1:PhyATK/2:MagATK/3:Star/4:Health)
    public int Value2; //Type2 Value
    public TwinCard(int _id,int _type1, int _value1, int[] _attackzone1, int _type2, int _value2):base(_id)
    {
        this.Type1 = _type1;
        this.Value1 = _value1;
        this.AttackZone1 = _attackzone1;
        this.Type2 = _type2;
        this.Value2 = _value2;
    }
}
public class HealthCard : Card
{
    public int Type; //Type1(0:Move/1:PhyATK/2:MagATK/3:Star/4:Health)
    public int Value; //Type1 Value

    public HealthCard(int _id,int _type, int _value):base(_id)
    {
        this.Type = _type;
        this.Value = _value;
    }
}
