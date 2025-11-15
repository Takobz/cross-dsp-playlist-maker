using CrossDSP.Infrastructure.Services.Spotify.Models;
using CrossDSP.WEBAPI.DTOs.Responses;

namespace CrossDSP.WEBAPI.Mappers
{
    public static class SpotifyResourcesToDTOs
    {
        public static IEnumerable<SongSearchResponse> ToSongSearchResponses(
            this SpotifyTrackSearchResponse response
        )
        {
            if (!response.Tracks.Items.Any()) return [];

            return response.Tracks.Items.Select(track =>
            {
                return new SongSearchResponse(
                    mainArtistName: track.Artists.First().ArtistName,
                    dsp: SpotifyConstants.Spotify,
                    dspSongId: track.EntityId,
                    songTitle: track.TrackName
                );
            });
        }

        public static IEnumerable<PlaylistResponse> ToPlaylistResponses(
            this IEnumerable<SpotifyPlaylist> playlists
        )
        {
            if (!playlists.Any()) return [];

            return playlists.Select(playlist =>
            {
                return new PlaylistResponse(
                    dsp: SpotifyConstants.Spotify,
                    id: playlist.EntityId,
                    name: playlist.Name,
                    description: playlist.Description
                );
            });
        }
    }
}