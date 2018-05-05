import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

public class Opening {

	public String code;
	public String name;
	public List<String> moves;
	
	public Opening()
	{
		moves = new ArrayList<String>();
	}
	
	public void addMove(String move)
	{
		moves.add(move);
	}
	
	public List<String> getMoves()
	{
		return moves;
	}
	
	public static Opening FromString(String openingString)
	{
		Opening rtn = new Opening();
		if (openingString == null || openingString.length() == 0)
		{
			return rtn;
		}
		int currentPosition = 0;
		String c = "";
		
		//extract code
		while (openingString.charAt(currentPosition) != ' ')
		{
			c = c.concat(Character.toString(openingString.charAt(currentPosition)));
			currentPosition++;
			if (openingString.length() == currentPosition)
			{
				rtn.code = c;
				return rtn;
			}
		}
		rtn.code = c;
		
		//extract opening name
		int quoteCount = 0;
		String openingName = "";
		
		while (quoteCount < 2)
		{
			if (quoteCount == 0)
			{
				if (openingString.charAt(currentPosition) == '\"')
					quoteCount++;
			}
			else if (quoteCount == 1)
			{
				if (openingString.charAt(currentPosition) == '\"')
				{
					quoteCount++;
				}
				else
				{
					openingName = openingName.concat(Character.toString(openingString.charAt(currentPosition)));
				}
			}
			currentPosition++;
			if (openingString.length() == currentPosition)
			{
				rtn.name = openingName;
				return rtn;
			}
		}
		
		rtn.name = openingName;
		
		//Extract Moves
		String moveString = openingString.substring(currentPosition);
		moveString = moveString.trim();
		String[] movesArray = moveString.split(" ");
		for (int i = 0; i < movesArray.length; i++)
		{
			String currentMove = movesArray[i];
			if (currentMove.length() > 0 && currentMove.charAt(0) != '*')
			{
				String[] splitByPeriod = currentMove.split(Pattern.quote("."));
				String notation = splitByPeriod[splitByPeriod.length - 1];
				rtn.addMove(notation);
			}
		}
		return rtn;
	}
}
