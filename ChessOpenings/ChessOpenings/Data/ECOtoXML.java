import java.io.*;
import org.jdom2.*;
import org.jdom2.output.Format;
import org.jdom2.output.XMLOutputter;

import java.util.*;

public class ECOtoXML {
	
	public static final String PATH = "./";
	public static final String FILENAME = "scid.eco";
	public static final String OUTPUTFILE = "openings.xml";
	public static String currentLine = null;
	public static BufferedReader br = null;
	public static FileReader fr = null;
	public static Document xmlDoc = null;
	public static Element root;
	public static List<Opening> allOpenings;
	public static List<Game> gameList;
	

	public static void main(String[] args) {
		Init();
		while (currentLine != null)
		{
			String currentOpeningString = getNextOpening();
			Opening opening = Opening.FromString(currentOpeningString);
			allOpenings.add(opening);
			System.out.println(currentOpeningString);
		}
		Element root = ConvertToXml(allOpenings);
		determineOpeningFrequencys(root, new ArrayList<String>());
		xmlDoc.setRootElement(root);
		OutputXML(xmlDoc, getFilePath(OUTPUTFILE));
		System.out.println("END");
	}
	
	public static String getNextOpening()
	{
		String rtn = "";
		try
		{
			boolean done = false;
			while (!done && currentLine != null)
			{
				if (currentLine.trim().endsWith("*"))
				{
					done = true;
				}
				if (!currentLine.startsWith("#") && currentLine.trim().length() > 0)
				{
					rtn = rtn.concat(currentLine);
				}
				currentLine = br.readLine();
			}
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
		return rtn;
	}
	
	public static void Init()
	{
		allOpenings = new ArrayList<Opening>();
		try
		{
			fr = new FileReader(getFilePath(FILENAME));
			br = new BufferedReader(fr);
			currentLine = br.readLine();
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
		PgnReader r = new PgnReader();
		System.out.println("Reading Games...");
		gameList = r.getGamesList();
		Element root = new Element("Openings");
		xmlDoc = new Document(root);
	}
	
	public static Element ConvertToXml(List<Opening> openings)
	{
		root = new Element("Start");
		root.setAttribute("Name", "Start");
		int openingCount = 1;
		for (Opening currentOpening : openings)
		{
			addOpening(currentOpening);
			if (openingCount % 100 == 0)
			{
				System.out.println("Converting opening " + openingCount + " to XML");
			}
			openingCount++;
		}
		return root;
	}
	
	public static Element ConvertToXml(String opening)
	{
		return new Element("Test");
	}
	
	public static void addOpening(Opening opening)
	{
		Element currentElement = root;
		for (String move : opening.getMoves())
		{
			boolean added = false;
			//while (!added)
			//{
				List<Element> eList = currentElement.getChildren();
				Element e = getChildForMove(eList, move);
				if (e == null)
				{
					Element child = new Element(opening.code);
					//child.setAttribute("Code", opening.code);
					child.setAttribute("Move", move);
					child.setAttribute("Name", opening.name);
					currentElement.addContent(child);
					added = true;
				}
				else
				{
					currentElement = e;
				}
			//}
		}
	}
	
	public static Element getChildForMove(List<Element> eList, String move)
	{
		for (Element e : eList)
		{
			if(e.getAttribute("Move").getValue().equals(move))
			{
				return e;
			}
		}
		return null;
	}
	
	public static void OutputXML(Document outDoc, String destination)
	{
		XMLOutputter xmlOutput = new XMLOutputter();
		try
		{
			xmlOutput.setFormat(Format.getPrettyFormat());
			xmlOutput.output(outDoc, new FileWriter(destination));
			System.out.println("Saved " + destination);
		}
		catch (Exception e)
		{
			System.out.println("Error writing file");
		}
	}

	public static void determineOpeningFrequencys(Element element, List<String> moves)
	{
		if (element.getAttributeValue("Move") != null)
		{
			moves.add(element.getAttributeValue("Move"));
			//System.out.println("Move added - " + element.getAttributeValue("Move"));
		}
		if (element.getAttributeValue("Name").equals("English"))
		{
			System.out.println("English found - Moves size = " + moves.size());
		}
		List<Element> children = element.getChildren();
		if (children == null || children.size() == 0) return;
		List<Integer> gameCountList = new ArrayList<Integer>();
		List<Float> successRateList = new ArrayList<Float>();
		//get the counts for each child opening
		for (Element e : children)
		{
			float successRate = 0;
			//List<Float> successCountList = new ArrayList<Float>();
			List<String> movesForChild = new ArrayList<String>();
			movesForChild.addAll(moves);
			movesForChild.add(e.getAttributeValue("Move"));
			int count = 0;
			float successCount = 0;
			for (Game g : gameList)
			{
				if (g.employsOpening(movesForChild))
				{
					count++;
					successCount += g.getResult();
				}
			}
			gameCountList.add(count);
			
			if (count > 0)
			{
				successRate = successCount / (float)count;
			}
			successRateList.add(successRate);
		}
		int total = 0;
		for (int i : gameCountList)
		{
			total += i;
		}
		for (int i = 0; i < children.size(); i++)
		{
			double proportion = 0;
			if (total != 0)
			{
				proportion = (double)(gameCountList.get(i)) / (double)total;
				//System.out.println("count = " + gameCountList.get(i) + " total = " + total + " frequency = " + proportion);
				
			}
			float successRate = successRateList.get(i);
			children.get(i).setAttribute("Frequency", Double.valueOf(proportion).toString());
			children.get(i).setAttribute("Success_Rate", Float.valueOf(successRate).toString());
			
		}
		for (Element child : children)
		{
			List<String> movesCopy = new ArrayList<String>();
			movesCopy.addAll(moves);
			determineOpeningFrequencys(child, movesCopy);
		}
	}
	
	public static String getFilePath(String fileName)
	{
		return PATH + fileName;
	}

}
