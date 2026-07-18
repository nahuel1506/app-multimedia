export interface Content {
    id: string;
    type: 'Movie' | 'Anime';
    title: string;
    description: string;
    coverImageUrl: string | null;
    releaseDate: string | null;
    genres: string[];
    createdAt: string;
}
