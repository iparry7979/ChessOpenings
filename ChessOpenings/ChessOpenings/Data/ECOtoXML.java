import java.io.*;
import org.jdom2.*;
import org.jdom2.output.Format;
import org.jdom2.output.XMLOutputter;

import java.util.*;

public class ECOtoXML {
	
	public static final String PATH = "D:/Dev/Data Files/Chess/";
	public static final String FILENAME = "scid.eco";
	public static final String OUTPUTFILE = "openings.xml";
	public static String currentLine = null;
	public static BufferedReader br = null;
	public static FileReader fr = null;
	public static Document xmlDoc = null;
	public static Element root;
	public static List<Opening> allOpenings;
	

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
	
	public static String getFilePath(String fileName)
	{
		return PATH + fileName;
	}

}
