export interface Book {
  id: number;
  title: string;
  year: number;
  isbn: string;
  coverImage: string;
  publisher: string;
  format: string;
  price: number;
  currency: string;
  inStock: number;
  rating: number;
  reviewCount: number;
  synopsis: string;
  targetAudience: string;
  readingTime: number;
  publicationDate: string;
  edition: string;
  dimensions: string;
  weight: number;
  salesRank: number;
  maturityRating: string;
  series: string;
  seriesOrder: string;
  tableOfContents: string;
  fileSize: string;
  wordCount: number;
  authorId: number;
  author: Author;
  genreId: number;
  genre: Genre;
  bookSubGenres: BookSubGenre[];
  bookTags: BookTag[];
  bookAwards: BookAward[];
}

export interface Author {
  id: number;
  name: string;
  bio: string;
  imageUrl: string;
}

export interface Genre {
  id: number;
  name: string;
}

export interface BookSubGenre {
  id: number;
  bookId: number;
  subGenreId: number;
  subGenre: SubGenre;
}

export interface SubGenre {
  id: number;
  name: string;
}

export interface BookTag {
  id: number;
  bookId: number;
  tagId: number;
  tag: Tag;
}

export interface Tag {
  id: number;
  name: string;
}

export interface BookAward {
  id: number;
  bookId: number;
  awardId: number;
  award: Award;
}

export interface Award {
  id: number;
  name: string;
}
