/* Q14.WJP  Given a string print the reverse of the wordsin the string.(Input:  Java Code Output: Code Java) */
import java.util.Scanner;

public class ReverseWords {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        // Input from the user
        System.out.print("Enter a string to reverse the words: ");
        String input = scanner.nextLine();

        // Reverse the words in the string
        String reversedWords = reverseWordsInString(input);

        // Print the reversed words string
        System.out.println("Reversed words string: " + reversedWords);

        scanner.close();
    }

    // Method to reverse the words in a given string
    public static String reverseWordsInString(String input) {
        String[] words = input.split("\\s+");
        StringBuilder reversed = new StringBuilder();

        for (int i = words.length - 1; i >= 0; i--) {
            reversed.append(words[i]);
            if (i != 0) {
                reversed.append(" ");
            }
        }

        return reversed.toString();
    }
}
