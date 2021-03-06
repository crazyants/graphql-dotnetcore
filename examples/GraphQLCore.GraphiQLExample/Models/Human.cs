﻿namespace GraphQLCore.GraphiQLExample.Models
{
    using System.Collections.Generic;

    public class Human : ICharacter
    {
        public IEnumerable<Episode> AppearsIn { get; set; }
        public IEnumerable<ICharacter> Friends { get; set; }
        public string HomePlanet { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string SecretBackstory { get; set; }
    }
}