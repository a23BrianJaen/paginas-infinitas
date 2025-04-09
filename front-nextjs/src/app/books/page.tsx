import ClientBooks from "@/components/ClientBooks"
import { getAllBooks } from "@/services/communicationManager"

export default async function Books() {

  const books = await getAllBooks()

  return (
    <ClientBooks books={books} />
  )
}