import java.util.*;

public class Game
{
	private List<String> moves = new ArrayList<String>();
	private float result; //0 = black, 0.5 = draw 1 = white 
	private String gameString;
	
	public Game(String gameString)
	{
		this.gameString = gameString;
		if (gameString.contains("1/2-1/2"))
		{
			result = 0.5f;
		}
		else if (gameString.contains("1-0"))
		{
			result = 1;
		}
		else if (gameString.contains("0-1"))
		{
			result = 0;
		}
		else
		{
			result = -1;
		}
		extractMoves();
	}
	
	public float getResult()
	{
		return result;
	}

	public String toString()
	{
		String winningString = "";
		if (result == 1) winningString = "White wins";
		if (result == 0.5) winningString = "Draw";
		if (result == 0) winningString = "Black wins";
		String rtn = moves.size() + " moves - " + winningString;
		return rtn;
	}

	public boolean employsOpening(List<String> openingSequence)
	{
		boolean rtn = true;
		for (int i = 0; i < openingSequence.size(); i++)
		{
			if (i < moves.size())
			{
				if (!openingSequence.get(i).equals(moves.get(i)))
				{
					rtn = false;
				}
			}
			else
			{
				return false;
			}
		}
		//System.out.println("match found = " + rtn);
		return rtn;
	}

	private void extractMoves()
	{
		String strippedString = gameString.replaceAll("\\[.*\\]", "");
		//strippedString = strippedString.replaceAll("{.*}", "");
		strippedString = strippedString.replaceAll("\\$\\d", "");
		strippedString = strippedString.replaceAll("\\d*\\.", "");
		strippedString = strippedString.replaceAll("1/2-1/2", "");
		strippedString = strippedString.replaceAll("1-0", "");
		strippedString = strippedString.replaceAll("0-1", "");

		//System.out.println("string to split " + strippedString);

		String[] movesArray = strippedString.split("\\s");
		//System.out.println(movesArray.length);
		for (int i = 0; i < movesArray.length; i++)
		{
			String currentString = movesArray[i];
			if (currentString.trim().length() != 0)
			{
				moves.add(currentString);
				//System.out.println("Adding move - " + currentString);
			}
		}
	}	
}
