using CsvHelper;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Options;
using System.Globalization;
using TelegramChannelAnalyzer.Configuration;
using TL;

namespace TelegramChannelAnalyzer.Services
{
    public interface IRetriveMessages
    {
        Task<bool> ParseMessage();      
    }

    public class MessageRetriever : IRetriveMessages
    {
        private readonly TelegramAuthenticationConfig _telegramAuthenticationOptions;
        private readonly TelegramChanneConfig _telegramChannelOptions;
        private readonly OutputFileConfig _outputFile;
        private readonly StopWordsConfig _stopWordsOptions;
        private readonly WTelegram.Client _telegramClient;

        public MessageRetriever(
            IOptions<TelegramAuthenticationConfig> telegramAuthenticationOptions,
            IOptions<TelegramChanneConfig> telegramChannelOptions,
            IOptions<OutputFileConfig> outputFile,
            IOptions<StopWordsConfig> stopWordsOptions
        )
        {
            _telegramAuthenticationOptions = telegramAuthenticationOptions.Value;
            _telegramChannelOptions = telegramChannelOptions.Value;
            _outputFile = outputFile.Value;
            _stopWordsOptions = stopWordsOptions.Value;
            _telegramClient = new WTelegram.Client(Config);
        }

        private string Config(string what)
        {
            switch (what)
            {
                case "api_id":
                    return _telegramAuthenticationOptions.ApiId;

                case "api_hash":
                    return _telegramAuthenticationOptions.ApiHash;

                case "phone_number":
                    return _telegramAuthenticationOptions.PhoneNumber;

                case "verification_code":
                    Console.Write("Code: ");
                    return Console.ReadLine();

                default:
                    return null;
            }
        }

        private async Task<InputPeer> RetrieveInputPeer()
        {
            await _telegramClient.LoginUserIfNeeded();
            var chats = await _telegramClient.Messages_GetAllChats(null);
            return chats.chats.FirstOrDefault(c => c.Key == _telegramChannelOptions.Id).Value.ToInputPeer();
        }

        public async Task<bool> ParseMessage()
        {
            var csvOutput = new List<CsvOutput>();
            var inputPeer = await RetrieveInputPeer();
            for (int offset = 0; ;)
            {
                var messagesBase = await _telegramClient.Messages_GetHistory(inputPeer, 0, default, offset, 1000, 0, 0, 0);
                if (messagesBase is not Messages_ChannelMessages channelMessages) break;
                foreach (var msgBase in channelMessages.messages)
                    if (msgBase is Message msg)
                    {
                        var msg_cast = (Message)msgBase;
                        Console.WriteLine($"{msg_cast.message} " + msg_cast.date);
                        var reaction = msg_cast.reactions?.results?.ToDictionary(r => string.Join("", System.Text.Encoding.Unicode.GetBytes(r.reaction)), r => r.count) ?? new Dictionary<string, int>();
                        csvOutput.Add(new CsvOutput
                        {
                            Message = CleanMessage(msg_cast.message),
                            MessageDate = msg_cast.date.ToString("yyyy-MM-ddTHH:mm:ss"),
                            Fire = GetEmoticonCount(reaction, EmojiEnum.Fire),
                            Clap = GetEmoticonCount(reaction, EmojiEnum.Clap),
                            Party = GetEmoticonCount(reaction, EmojiEnum.Party),
                            Approve = GetEmoticonCount(reaction, EmojiEnum.Approve),
                            Love = GetEmoticonCount(reaction, EmojiEnum.Love),
                            SmilingWithHearth = GetEmoticonCount(reaction, EmojiEnum.SmilingWithHearth),
                            Thinking = GetEmoticonCount(reaction, EmojiEnum.Thinking),
                            Crying = GetEmoticonCount(reaction, EmojiEnum.Crying),
                            Beaming = GetEmoticonCount(reaction, EmojiEnum.Beaming),
                            Horrified = GetEmoticonCount(reaction, EmojiEnum.Horrified),
                            Shit = GetEmoticonCount(reaction, EmojiEnum.Shit),
                            ExplodingHead = GetEmoticonCount(reaction, EmojiEnum.ExplodingHead),
                            Vomiting = GetEmoticonCount(reaction, EmojiEnum.Vomiting),
                            Dislike = GetEmoticonCount(reaction, EmojiEnum.Dislike),
                            Angry = GetEmoticonCount(reaction, EmojiEnum.Angry),
                            StarStruck = GetEmoticonCount(reaction, EmojiEnum.StarStruck),
                            Views = msg_cast.views
                        });
                    }
                offset += channelMessages.messages.Length;
                if (offset >= channelMessages.count) break;
            }
            using (var writer = new StreamWriter(Path.Combine(_outputFile.Path, _outputFile.Name)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(csvOutput);
            }
            return true;
        }

        public int GetEmoticonCount(Dictionary<string, int> reaction, string emojiEnum)
        {
            switch (emojiEnum)
            {
                case EmojiEnum.Fire:
                    return reaction.TryGetValue(EmojiEnum.Fire, out int fireCount) ? fireCount : 0;
                case EmojiEnum.Clap:
                    return reaction.TryGetValue(EmojiEnum.Clap, out int clapCount) ? clapCount : 0;
                case EmojiEnum.Party:
                    return reaction.TryGetValue(EmojiEnum.Party, out int partyCount) ? partyCount : 0;
                case EmojiEnum.Approve:
                    return reaction.TryGetValue(EmojiEnum.Party, out int ApproveCount) ? ApproveCount : 0;
                case EmojiEnum.Love:
                    return reaction.TryGetValue(EmojiEnum.Party, out int loveCount) ? loveCount : 0;
                case EmojiEnum.SmilingWithHearth:
                    return reaction.TryGetValue(EmojiEnum.SmilingWithHearth, out int smilingWithHearthCount) ? smilingWithHearthCount : 0;
                case EmojiEnum.Thinking:
                    return reaction.TryGetValue(EmojiEnum.Thinking, out int thinkingCount) ? thinkingCount : 0;
                case EmojiEnum.Crying:
                    return reaction.TryGetValue(EmojiEnum.Crying, out int cryingCount) ? cryingCount : 0;
                case EmojiEnum.Beaming:
                    return reaction.TryGetValue(EmojiEnum.Beaming, out int beamingCount) ? beamingCount : 0;
                case EmojiEnum.Horrified:
                    return reaction.TryGetValue(EmojiEnum.Horrified, out int horrifiedCount) ? horrifiedCount : 0;
                case EmojiEnum.Shit:
                    return reaction.TryGetValue(EmojiEnum.Shit, out int shitCount) ? shitCount : 0;
                case EmojiEnum.ExplodingHead:
                    return reaction.TryGetValue(EmojiEnum.ExplodingHead, out int explodingHeadCount) ? explodingHeadCount : 0;
                case EmojiEnum.Vomiting:
                    return reaction.TryGetValue(EmojiEnum.Vomiting, out int vomitingCount) ? vomitingCount : 0;
                case EmojiEnum.Dislike:
                    return reaction.TryGetValue(EmojiEnum.Dislike, out int dislikeCount) ? dislikeCount : 0;
                case EmojiEnum.Angry:
                    return reaction.TryGetValue(EmojiEnum.Angry, out int angryCount) ? angryCount : 0;
                case EmojiEnum.StarStruck:
                    return reaction.TryGetValue(EmojiEnum.StarStruck, out int starStruckCount) ? starStruckCount : 0;
                default:
                    return 0;
            }
        }

        public string CleanMessage(string message)
        {
            var stopWords = StopWord.StopWords.GetStopWords(CultureInfo.GetCultureInfo(_stopWordsOptions.CultureName));
            var splittedMessage = message.Split(' ');
            return string.Join(' ', splittedMessage.Except(stopWords));
        }
    }
}
