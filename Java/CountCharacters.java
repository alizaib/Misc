import java.util.HashMap;
import java.util.Scanner;

public class CountCharacters {
    public static void main(String[] args) {
		Scanner in = new Scanner(System.in);
		System.out.print("Enter the input string: ");
		String input = in.nextLine();		
		int number = Integer.parseInt(input);
		
		System.out.println("You entered: " + number);
		System.out.println("when multipled by 2: " + number*2);
        
        // Call the method to count characters
        //countCharacters(input);
    }
    
    public static void countCharacters(String input) {
        HashMap<Character, Integer> charCountMap = new HashMap<>(); //DataHolder that can hold a char and a number
        int repeatedIntegers = 0;
        int uppercaseCount = 0;
        int lowercaseCount = 0;

        // Populate the HashMap with character counts
        for (char c : input.toCharArray()) {
            if (Character.isDigit(c) || Character.isUpperCase(c) || Character.isLowerCase(c)) {
                charCountMap.put(c, charCountMap.getOrDefault(c, 0) + 1);
            }
        }

        // Calculate the number of repeated integers, uppercase, and lowercase characters
        for (char c : charCountMap.keySet()) {
            int count = charCountMap.get(c);
            if (Character.isDigit(c) && count > 1) {
                repeatedIntegers++;
            }
            if (Character.isUpperCase(c)) {
                uppercaseCount++;
            }
            if (Character.isLowerCase(c)) {
                lowercaseCount++;
            }
        }

        // Print the results
        System.out.println("Total number of repeated integers: " + repeatedIntegers);
        System.out.println("Total number of uppercase characters: " + uppercaseCount);
        System.out.println("Total number of lowercase characters: " + lowercaseCount);
    }
}