import java.io.*;
import java.util.*;

public class PgnReader
{
	public static final String PATH = "D:/Dev/Data Files/Chess/";
	public static final String FILENAME = "ficsgamesdb_201801_standard2000_nomovetimes_1543578.pgn";
	public BufferedReader br = null;
	public FileReader fr = null;
	public List<Game> games = new ArrayList<Game>();


	public List<Game> getGamesList()
	{
		try
		{
			fr = new FileReader(PATH + FILENAME);
			br = new BufferedReader(fr);
		
		
			String currentGame = "";

			String currentLine = br.readLine();
			while (currentLine != null)
			{
				currentGame += currentLine + "\n";
				if (currentLine.endsWith("1/2-1/2") || currentLine.endsWith("1-0") || currentLine.endsWith("0-1"))
				{
					games.add(new Game(currentGame));
					//currentGame = trimGame(currentGame);
					//games.add(currentGame);
					currentGame = "";
				}
				currentLine = br.readLine();
			}
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
		for (Game g : games)
		{
			System.out.println("game :- " + g.toString());
		}
		return games;
	}

	private String trimGame(String game)
	{
		String rtn;
		rtn = game.replaceAll("\\[.*\\]", "");
		rtn = game.replaceAll("{.*}", "");
		rtn = game.replaceAll("\\$\\d", "");
		rtn = game.replaceAll("\\d*\\.", "");
		return rtn;
	}

}
