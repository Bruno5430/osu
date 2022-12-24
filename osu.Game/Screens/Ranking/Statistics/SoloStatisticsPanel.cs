// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Online.Solo;
using osu.Game.Scoring;
using osu.Game.Screens.Ranking.Statistics.User;

namespace osu.Game.Screens.Ranking.Statistics
{
    public partial class SoloStatisticsPanel : StatisticsPanel
    {
        private readonly ScoreInfo achievedScore;

        public SoloStatisticsPanel(ScoreInfo achievedScore)
        {
            this.achievedScore = achievedScore;
        }

        public Bindable<SoloStatisticsUpdate?> StatisticsUpdate { get; } = new Bindable<SoloStatisticsUpdate?>();

        protected override ICollection<StatisticRow> CreateStatisticRows(ScoreInfo newScore, IBeatmap playableBeatmap)
        {
            var rows = base.CreateStatisticRows(newScore, playableBeatmap);

            if (newScore.UserID == achievedScore.UserID && newScore.OnlineID == achievedScore.OnlineID)
            {
                rows = rows.Append(new StatisticRow
                {
                    Columns = new[]
                    {
                        new StatisticItem("Overall Ranking", () => new OverallRanking
                        {
                            RelativeSizeAxes = Axes.X,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Width = 0.5f,
                            StatisticsUpdate = { BindTarget = StatisticsUpdate }
                        })
                    }
                }).ToArray();
            }

            return rows;
        }
    }
}
