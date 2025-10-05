namespace App;

//class for trade creation

public class Trade
{
  public string Sender;
  public string Receiver;
  public ENum_TradeStatus Status;
  public List<string> ItemsNames;


  public Trade(string sender, string receiver, ENum_TradeStatus status, List<string> itemNames)
  {
    Sender = sender;
    Receiver = receiver;
    Status = status;
    ItemsNames = itemNames;
  }
}
