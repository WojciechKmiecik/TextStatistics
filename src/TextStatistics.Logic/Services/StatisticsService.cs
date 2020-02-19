using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TextStatistics.Definition;
using TextStatistics.Definition.DataServices;
using TextStatistics.Definition.Services;

namespace TextStatistics.Logic.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticDataService _dataService;
        private readonly ILogger<StatisticsService> _logger;

        public StatisticsService(IStatisticDataService dataService, ILogger<StatisticsService> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }
        public async Task<string> GenerateNewStatistics(string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("input is null or empty, fix it!");
            }
            // im doing it in two loops
            string guid = Guid.NewGuid().ToString();
            long internalId = 0;
            try
            {
                internalId = await _dataService.AddStatistics(new Definition.Models.TextStatistics() { Guid = guid });
            }
            catch (Exception e)
            {
                _logger.LogError("Cannot store information to database");

            }
#pragma warning disable 4014
            Task.Factory.StartNew(() =>
            {
                DoWorkAsync(input, internalId);
            });
#pragma warning restore 4014

            return guid;

        }
        public async Task<TextStatistics.Definition.Models.TextStatistics> GetStatistics(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                return null;
            }
            try
            {
                return await _dataService.Get(guid);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
        private async void DoWorkAsync(string input, long id)
        {
            uint wordCount ;
            uint hyphenCount;
            uint spaceCount;
            try
            {
                hyphenCount = await GetHyphens(input);
                var wordsAndSpaces = await CountWordsAndSpaces(input);
                wordCount = wordsAndSpaces.Item1;
                spaceCount = wordsAndSpaces.Item2;

                await UpdateToDatabase(new TextStatistics.Definition.Models.TextStatistics()
                {
                    Id = id,
                    HyphenCount = hyphenCount,
                    SpacesCount = spaceCount,
                    WordCount = wordCount
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot calculate words, argument error. Reason " + ex.Message);
            }
        }

        private async Task UpdateToDatabase(Definition.Models.TextStatistics textStatistics)
        {
            await _dataService.UpdateStatistics(textStatistics);
        }

        private async Task<uint> GetHyphens(string input)
        {
            uint hyphenCount = 0;
            foreach (var letter in input.ToCharArray())
            {
                if (letter == Consts.WordCounting.Hyphen)
                {
                    hyphenCount += 1;
                    continue;
                }
            }
            await Task.CompletedTask;
            return hyphenCount;
        }
        private async Task<ValueTuple<uint, uint>> CountWordsAndSpaces(string input)
        {
            uint wordCount = 0;
            uint spaceCount = 0;

            int index = 0;

            // skip whitespace until first word
            try
            {
                while (index < input.Length && char.IsWhiteSpace(input[index]))
                {
                    index++;
                    spaceCount++;
                }

                while (index < input.Length)
                {
                    // check if current char is part of a word
                    while (index < input.Length && !char.IsWhiteSpace(input[index]))
                        index++;

                    wordCount++;

                    // skip whitespace until next word
                    while (index < input.Length && char.IsWhiteSpace(input[index]))
                    {
                        index++;
                        spaceCount++;
                    }
                }
            }
            catch (Exception e)
            {
                // log, check which exception type
                throw;
            }
            await Task.CompletedTask;
            return new ValueTuple<uint, uint>(wordCount, spaceCount);
        }
    }
}
