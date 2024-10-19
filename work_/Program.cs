using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace DiscordAutomation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Download Chromium if necessary
            await new BrowserFetcher().DownloadAsync();

            // Launch the Chromium browser (log in to Discord manually if necessary)
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false,  // Set to true to run without a visible browser window
            });

            var page = await browser.NewPageAsync();

            // Navigate to the Discord channel page
            await page.GoToAsync("https://discord.com/channels/1293309437100036137/1295587634801803356");

            // Wait for the message input field to appear
            await page.WaitForSelectorAsync("div[role='textbox']");

            // Loop indefinitely to send the commands in the correct order every 3 minutes
            while (true)
            {
                // Focus the message input field
                await page.FocusAsync("div[role='textbox']");

                // Command 1: Type '!work' and press Enter
                await page.Keyboard.TypeAsync("!work");
                await page.Keyboard.PressAsync("Enter");
                Console.WriteLine("Sent '!work' at: " + DateTime.Now);

                // Wait 1 second before sending the next command
                await Task.Delay(2000);

                // Command 2: Type '!dep all' and press Enter
                await page.Keyboard.TypeAsync("!dep all");
                await page.Keyboard.PressAsync("Enter");
                Console.WriteLine("Sent '!dep all' at: " + DateTime.Now);

                // Wait 1 second before sending the next command
                await Task.Delay(1500);

                // Command 3: Type '!withdraw 100' and press Enter
                await page.Keyboard.TypeAsync("!withdraw 1000");
                await page.Keyboard.PressAsync("Enter");
                Console.WriteLine("Sent '!withdraw 1000' at: " + DateTime.Now);

                // Wait 1 second before sending the next command
                await Task.Delay(1500);

                // Command 4: Type '!roulette 100 red' and press Enter
                await page.Keyboard.TypeAsync("!roulette 1000 red");
                await page.Keyboard.PressAsync("Enter");
                Console.WriteLine("Sent '!roulette 1000 red' at: " + DateTime.Now);

                // Wait 30 seconds for the result message
                await Task.Delay(32000);  // 30,000 ms = 30 seconds

                // Check the last message for the result
                var lastMessage = await page.EvaluateExpressionAsync<string>(
                    "document.querySelectorAll('[id^=message-content]')[document.querySelectorAll('[id^=message-content]').length - 1].textContent"
                );

                Console.WriteLine("Last message received: " + lastMessage);

                // Based on the result, send the appropriate emoji
                if (lastMessage.Contains("Winners:"))
                {
                    // Send the ':Ahhhh' emoji
                    await page.Keyboard.TypeAsync(":Ahhhh");
                    await page.Keyboard.PressAsync("Enter");  // First Enter to bring up the emoji suggestion
                    await Task.Delay(500);  // Short delay between Enter presses
                    await page.Keyboard.PressAsync("Enter");  // Second Enter to select and send the emoji
                    Console.WriteLine("Sent ':Ahhhh' emoji at: " + DateTime.Now);
                    await page.Keyboard.TypeAsync("!dep all");
                    await page.Keyboard.PressAsync("Enter");
                    Console.WriteLine("Sent '!dep all' at: " + DateTime.Now);
                }
                else if (lastMessage.Contains("No Winners"))
                {
                    // Send the ':LilSad' emoji
                    await page.Keyboard.TypeAsync(":LilSad");
                    await page.Keyboard.PressAsync("Enter");  // First Enter to bring up the emoji suggestion
                    await Task.Delay(500);  // Short delay between Enter presses
                    await page.Keyboard.PressAsync("Enter");  // Second Enter to select and send the emoji
                    Console.WriteLine("Sent ':lilSad' emoji at: " + DateTime.Now);
                }

                // Command 2: Type '!dep all' and press Enter
                

                // Wait * minutes (148 seconds) before repeating the whole sequence
                await Task.Delay(148000);  // 148,000 ms = * minutes
            }
        }
    }
}
