package com.chocoretostonus.okaimono

import android.content.Context
import androidx.room.Room
import androidx.test.core.app.ApplicationProvider
import androidx.test.ext.junit.runners.AndroidJUnit4
import com.chocoretostonus.okaimono.data.notes.local.NoteDataBase
import com.chocoretostonus.okaimono.data.notes.local.Note_DAO
import com.chocoretostonus.okaimono.data.notes.model.NoteEntity
import org.junit.After
import org.junit.Assert
import org.junit.Before
import org.junit.Test
import org.junit.runner.RunWith
import java.io.IOException

@RunWith(AndroidJUnit4::class)
class NoteDataBaseInstrumentedTest {
    private lateinit var noteDao: Note_DAO
    private lateinit var db: NoteDataBase

    @Before
    fun createDb() {
        val context = ApplicationProvider.getApplicationContext<Context>()
        db = Room.inMemoryDatabaseBuilder(
            context, NoteDataBase::class.java).build()
        noteDao = db.noteDao()
    }

    @After
    @Throws(IOException::class)
    fun closeDb() {
        db.close()
    }

    @Test
    @Throws(Exception::class)
    fun writeNoteAndReadAllNotes() {
        val title = "Viaje"

        //Creamos una instacia de la clase Note_Entity
        val noteEntity = NoteEntity(1, "Viaje", "Alistar Comida")

        //Insertamos la nota en la base de datos de notas
        noteDao.insert(noteEntity = noteEntity)

        //Leemos la base de datos de notas
        val notes = noteDao.getAll()

        //borramos la nota de la base de datos de notas
        noteDao.delete(noteEntity = noteEntity)

        //Mostramos la lista de notas en la consola
        println(notes)

        //Comprobamos que el resultado sea el esperado
        Assert.assertEquals(notes[0].Name, title)
    }
}