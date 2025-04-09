import { Book } from "@/types/types";

export const getAllBooks = async (): Promise<Book[]> => {
  const URL = "http://localhost:5000/api/books"
  const res = await fetch(URL)
  const json = await res.json()
  return json
}