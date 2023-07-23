package com.chocoretostonus.okaimono.domain.notes

import com.chocoretostonus.okaimono.data.notes.NoteRepository
import com.chocoretostonus.okaimono.data.notes.model.NoteEntity
import com.chocoretostonus.okaimono.domain.notes.model.Note
import com.chocoretostonus.okaimono.domain.notes.model.toNote
import com.chocoretostonus.okaimono.domain.notes.model.toNoteEntity
import javax.inject.Inject

class AddNote @Inject constructor(private val noteRepository: NoteRepository){
    suspend operator fun invoke(note:Note){
        return noteRepository.insert(noteEntity = note.toNoteEntity())
    }
}