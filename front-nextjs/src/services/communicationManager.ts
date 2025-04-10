import { Book } from "@/types/types";

const API_URL = process.env.NEXT_PUBLIC_API_URL_NET

export const getAllBooks = async (): Promise<Book[]> => {
  const res = await fetch(`${API_URL}`)
  const json = await res.json()
  return json
}

export const getBookById = async (id: number): Promise<Book> => {
  const res = await fetch(`${API_URL}/${id}`)
  const json = await res.json()
  return json
}