package com.chocoretostonus.okaimono.di

import android.content.Context
import androidx.room.Room
import com.chocoretostonus.okaimono.data.notes.local.NoteDataBase
import com.chocoretostonus.okaimono.data.notes.local.Note_DAO
import com.chocoretostonus.okaimono.utlis.Constants
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import javax.inject.Singleton

@InstallIn(SingletonComponent::class)
@Module
object AppModule {


    @Provides
    @Singleton
    fun provideNoteDataBase(@ApplicationContext app: Context): NoteDataBase = Room.databaseBuilder(
            app,
            NoteDataBase::class.java,
            Constants.note_database
        ).build()

    @Provides
    @Singleton
    fun provideNoteDao(noteDataBase: NoteDataBase): Note_DAO = noteDataBase.noteDao()
}