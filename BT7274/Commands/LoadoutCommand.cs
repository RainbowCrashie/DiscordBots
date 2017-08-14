using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using BT7274.DataServices;
using BT7274.MilitiaHeadquarters;
using BT7274.MilitiaHeadquarters.Extentions;
using Discord.Commands;
using DiscordNetPlus.Commands;
using DiscordNetPlus.Extentions;
using Image = System.Drawing.Image;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace BT7274.Commands
{
    [Group("lo"), Summary("戦況に応じて適切な装備を提案します。")]
    public class LoadoutCommand : CommandBase
    {
        #region Fields and Properties

        private static LoadoutData LoadoutData { get; } = Locator.LoadoutData;
        private static string IconsPath { get; } = "Assets/LoadoutIcons";

        private static string PilotBackgroundFile { get; } = "PilotBackground";
        private static string TitanBackgroundFile { get; } = "TitanBackground";

        #endregion

        #region Methods

        [Command("pilot"), Alias("")]
        public async Task GeneratePilotLoadout()
        {
            using (TypingIndicator)
            {
                await Context.Log("Loadout Command Executed.");

                StartStopwatch();
                
                var tacticalName = LoadoutData.PilotLoadout.Tacticals.RandomPick();
                var primaryName = LoadoutData.PilotLoadout.Primaries.RandomPick();
                var secondaryName = LoadoutData.PilotLoadout.Secondaries.RandomPick();
                var ordnanceName = LoadoutData.PilotLoadout.Ordnances.RandomPick();
                var perk1Name = LoadoutData.PilotLoadout.Perks1.RandomPick();
                var perk2Name = LoadoutData.PilotLoadout.Perks2.RandomPick();
                var executionName = LoadoutData.PilotLoadout.Executions.RandomPick();
                var boostName = LoadoutData.PilotLoadout.Boosts.RandomPick();

                var imageBackground = GetIcon(PilotBackgroundFile);

                await RecordStopwatch("Random Pick");

                var img = new Bitmap(imageBackground.Width, imageBackground.Height);
                using (var gr = Graphics.FromImage(img))
                {
                    gr.SmoothingMode = SmoothingMode.HighQuality;

                    gr.DrawImage(imageBackground, new Rectangle(new Point(0, 0), new Size(651, 426)));

                    gr.DrawImage(GetIcon(tacticalName), new Rectangle(new Point(12, 96), new Size(166, 82)));
                    gr.DrawImage(GetIcon(primaryName), new Rectangle(new Point(12, 214), new Size(166, 82)));
                    gr.DrawImage(GetIcon(secondaryName), new Rectangle(new Point(12, 330), new Size(166, 82)));

                    gr.DrawImage(GetIcon(ordnanceName), new Rectangle(new Point(184, 96), new Size(70, 70)));

                    gr.DrawImage(GetIcon(perk1Name), new Rectangle(new Point(356, 96), new Size(70, 70)));
                    gr.DrawImage(GetIcon(executionName), new Rectangle(new Point(356, 214), new Size(70, 70)));
                    gr.DrawImage(GetIcon(boostName), new Rectangle(new Point(356, 330), new Size(166, 82)));

                    gr.DrawImage(GetIcon(perk2Name), new Rectangle(new Point(528, 96), new Size(70, 70)));

                    await RecordStopwatch("Rendering Icons");


                    var standardFont = new Font(FontFamily.GenericSansSerif, 11);
                    var brush = Brushes.White;

                    gr.DrawString(tacticalName, standardFont, brush, 12, 96 - 19);
                    gr.DrawString(primaryName, standardFont, brush, 12, 214 - 19);
                    gr.DrawString(secondaryName, standardFont, brush, 12, 330 - 19);

                    gr.DrawString(ordnanceName, standardFont, brush, 184, 96 - 19);

                    gr.DrawString(perk1Name, standardFont, brush, 356, 96 - 19);
                    gr.DrawString(executionName, standardFont, brush, 356, 214 - 19);
                    gr.DrawString(boostName, standardFont, brush, 356, 330 - 19);

                    gr.DrawString(perk2Name, standardFont, brush, 528, 96 - 19);

                    gr.DrawString($"@{Context.User.Username}", new Font(FontFamily.GenericSansSerif, 25), brush, 11, 16);

                    await RecordStopwatch("Rendering Captions");
                }

                await SendImageResultAsync(img);
                img.Dispose();
            }
        }

        [Command("titan")]
        public async Task GenerateTitanLoadout()
        {
            using (TypingIndicator)
            {

                await Context.Log("Loadout Command Executed.");
                StartStopwatch();

                var titan = LoadoutData.TitanLoadout.Titans.RandomPick();

                var titanName = titan.ToString();
                var commonkitName = LoadoutData.TitanLoadout.CommonKits.RandomPick();
                var uniqueKitName = titan.UniqueKits.RandomPick();
                var fallkitName = LoadoutData.TitanLoadout.FallKits.RandomPick();

                var imageBackground = GetIcon(TitanBackgroundFile);
                
                await RecordStopwatch("Random Pick");

                var img = new Bitmap(imageBackground.Width, imageBackground.Height);
                using (var gr = Graphics.FromImage(img))
                {
                    gr.SmoothingMode = SmoothingMode.HighQuality;

                    gr.DrawImage(imageBackground, new Rectangle(new Point(0, 0), new Size(651, 426)));
                    gr.DrawImage(GetIcon(titanName), new Rectangle(new Point(216, 42), new Size(434, 384)));

                    gr.DrawImage(GetIcon(commonkitName), new Rectangle(new Point(12, 96), new Size(70, 70)));
                    gr.DrawImage(GetIcon(uniqueKitName), new Rectangle(new Point(12, 214), new Size(70, 70)));
                    gr.DrawImage(GetIcon(fallkitName), new Rectangle(new Point(12, 330), new Size(70, 70)));

                    await RecordStopwatch("Rendering Icons");

                    var standardFont = new Font(FontFamily.GenericSansSerif, 11);
                    var brush = Brushes.White;
                    var orangeBrush = new SolidBrush(Color.FromArgb(254, 185, 0));

                    gr.DrawString("タイタンキット", standardFont, brush, 12 + 70 + 6, 96);
                    gr.DrawString(commonkitName, standardFont, orangeBrush, 12 + 70 + 6, 96 + 20);

                    gr.DrawString($"{titanName}キット", standardFont, brush, 12 + 70 + 6, 214);
                    gr.DrawString(uniqueKitName, standardFont, orangeBrush, 12 + 70 + 6, 214 + 20);

                    gr.DrawString("タイタンフォールキット", standardFont, brush, 12 + 70 + 6, 330);
                    gr.DrawString(fallkitName, standardFont, orangeBrush, 12 + 70 + 6, 330 + 20);

                    gr.DrawString($"{titanName}", new Font(FontFamily.GenericSansSerif, 25), brush, 11, 16);

                    await RecordStopwatch("Rendering Captions");
                }

                await SendImageResultAsync(img);
                img.Dispose();
            }
        }
        
        private async Task SendImageResultAsync(Image image)
        {
            await Context.Channel.SendFileAsync(
                image.ToStream(ImageFormat.Png), $"Loadout{Context.User.Username}.png",
                $"パイロット {Context.User.Username} の推奨装備です。");
        }


        private static Image GetIcon(string name)
        {
            return Image.FromFile(Path.Combine(IconsPath, $"{name}.png"));
        }

        #endregion

    }
}