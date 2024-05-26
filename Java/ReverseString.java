/* Q13. Given a string print the reverse of the string.(Input:  Java Code Output: edoC avaJ) */
import java.util.Scanner;

public class ReverseString {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        // Input from the user
        System.out.println("Enter a string to reverse:");
        String input = scanner.nextLine();

        // Reverse the string
        String reversed = reverseString(input);

        // Print the reversed string
        System.out.println("Reversed string: " + reversed);

        scanner.close();
    }

    // Method to reverse a given string
    public static String reverseString(String input) {
        StringBuilder reversed = new StringBuilder(input);
        return reversed.reverse().toString();
    }
}
