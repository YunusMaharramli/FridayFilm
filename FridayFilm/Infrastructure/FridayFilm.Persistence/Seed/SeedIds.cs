using System;

namespace FridayFilm.Persistence.Seed;

/// <summary>
/// Seed məlumatı üçün sabit Id-lər. HasData deterministik dəyər tələb edir,
/// ona görə bütün Guid-lər burada bir yerdə saxlanılır.
/// </summary>
public static class SeedIds
{
    public static readonly DateTime SeedDate =
        new(2026, 9, 14, 0, 0, 0, DateTimeKind.Utc);

    public static class Languages
    {
        public static readonly Guid Azerbaijani = new("aaaa0000-0000-0000-0000-000000000001");
        public static readonly Guid English = new("aaaa0000-0000-0000-0000-000000000002");
        public static readonly Guid Russian = new("aaaa0000-0000-0000-0000-000000000003");
        public static readonly Guid Turkish = new("aaaa0000-0000-0000-0000-000000000004");
    }

    public static class Genres
    {
        public static readonly Guid Action = new("bbbb0000-0000-0000-0000-000000000001");
        public static readonly Guid Adventure = new("bbbb0000-0000-0000-0000-000000000002");
        public static readonly Guid ScienceFiction = new("bbbb0000-0000-0000-0000-000000000003");
        public static readonly Guid Drama = new("bbbb0000-0000-0000-0000-000000000004");
        public static readonly Guid Thriller = new("bbbb0000-0000-0000-0000-000000000005");
        public static readonly Guid Crime = new("bbbb0000-0000-0000-0000-000000000006");
        public static readonly Guid Comedy = new("bbbb0000-0000-0000-0000-000000000007");
        public static readonly Guid Romance = new("bbbb0000-0000-0000-0000-000000000008");
        public static readonly Guid Mystery = new("bbbb0000-0000-0000-0000-000000000009");
        public static readonly Guid Fantasy = new("bbbb0000-0000-0000-0000-000000000010");
        public static readonly Guid Historical = new("bbbb0000-0000-0000-0000-000000000011");
        public static readonly Guid Biography = new("bbbb0000-0000-0000-0000-000000000012");
    }

    public static class Categories
    {
        public static readonly Guid Action = new("11111111-1111-1111-1111-111111111111");
        public static readonly Guid Comedy = new("22222222-2222-2222-2222-222222222222");
        public static readonly Guid Drama = new("33333333-3333-3333-3333-333333333333");
        public static readonly Guid Horror = new("44444444-4444-4444-4444-444444444444");
        public static readonly Guid ScienceFiction = new("55555555-5555-5555-5555-555555555555");
        public static readonly Guid Romance = new("66666666-6666-6666-6666-666666666666");
        public static readonly Guid Thriller = new("77777777-7777-7777-7777-777777777777");
        public static readonly Guid Documentary = new("88888888-8888-8888-8888-888888888888");
        public static readonly Guid Fantasy = new("99999999-9999-9999-9999-999999999999");
        public static readonly Guid Animation = new("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        public static readonly Guid Mystery = new("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        public static readonly Guid Adventure = new("cccccccc-cccc-cccc-cccc-cccccccccccc");
        public static readonly Guid Crime = new("dddddddd-dddd-dddd-dddd-dddddddddddd");
        public static readonly Guid Family = new("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
        public static readonly Guid Historical = new("ffffffff-ffff-ffff-ffff-ffffffffffff");
    }

    public static class Actors
    {
        public static readonly Guid LeonardoDiCaprio = new("11111111-2222-3333-4444-555555555501");
        public static readonly Guid ScarlettJohansson = new("11111111-2222-3333-4444-555555555502");
        public static readonly Guid CillianMurphy = new("11111111-2222-3333-4444-555555555503");
        public static readonly Guid MargotRobbie = new("11111111-2222-3333-4444-555555555504");
        public static readonly Guid TomHardy = new("11111111-2222-3333-4444-555555555505");
        public static readonly Guid MerylStreep = new("11111111-2222-3333-4444-555555555506");
        public static readonly Guid KeanuReeves = new("11111111-2222-3333-4444-555555555507");
        public static readonly Guid NataliePortman = new("11111111-2222-3333-4444-555555555508");
        public static readonly Guid ChristianBale = new("11111111-2222-3333-4444-555555555509");
        public static readonly Guid CharlizeTheron = new("11111111-2222-3333-4444-555555555510");
    }

    public static class Directors
    {
        public static readonly Guid ChristopherNolan = new("22222222-3333-4444-5555-666666666601");
        public static readonly Guid QuentinTarantino = new("22222222-3333-4444-5555-666666666602");
        public static readonly Guid GretaGerwig = new("22222222-3333-4444-5555-666666666603");
        public static readonly Guid LanaWachowski = new("22222222-3333-4444-5555-666666666604");
        public static readonly Guid GeorgeMiller = new("22222222-3333-4444-5555-666666666605");
        public static readonly Guid DarrenAronofsky = new("22222222-3333-4444-5555-666666666606");
        public static readonly Guid MartinScorsese = new("22222222-3333-4444-5555-666666666607");
        public static readonly Guid DenisVilleneuve = new("22222222-3333-4444-5555-666666666608");
    }

    public static class Movies
    {
        public static readonly Guid Inception = new("cccc0000-0000-0000-0000-000000000001");
        public static readonly Guid TheDarkKnight = new("cccc0000-0000-0000-0000-000000000002");
        public static readonly Guid Oppenheimer = new("cccc0000-0000-0000-0000-000000000003");
        public static readonly Guid Interstellar = new("cccc0000-0000-0000-0000-000000000004");
        public static readonly Guid PulpFiction = new("cccc0000-0000-0000-0000-000000000005");
        public static readonly Guid DjangoUnchained = new("cccc0000-0000-0000-0000-000000000006");
        public static readonly Guid Barbie = new("cccc0000-0000-0000-0000-000000000007");
        public static readonly Guid LittleWomen = new("cccc0000-0000-0000-0000-000000000008");
        public static readonly Guid TheMatrix = new("cccc0000-0000-0000-0000-000000000009");
        public static readonly Guid MadMaxFuryRoad = new("cccc0000-0000-0000-0000-000000000010");
        public static readonly Guid BlackSwan = new("cccc0000-0000-0000-0000-000000000011");
        public static readonly Guid TheWolfOfWallStreet = new("cccc0000-0000-0000-0000-000000000012");
    }
}
