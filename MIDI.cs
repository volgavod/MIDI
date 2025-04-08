using NAudio.Midi;

static class MIDI
{
	public static void Run()
	{
		string outputFile = "C:\\Users\\Deft\\Desktop\\melody.mid";
		
		Dictionary<string, int> noteMapping = new()
		{
			{"C4", 60}, {"C#4", 61}, {"D4", 62}, {"D#4", 63}, {"E4", 64}, {"F4", 65}, {"F#4", 66}, {"G4", 67}, {"G#4", 68},
			{"A4", 69}, {"A#4", 70}, {"B4", 71}, {"C5", 72}, {"C#5", 73}, {"D5", 74}, {"D#5", 75}, {"E5", 76}, {"F5", 77},
			{"F#5", 78}, {"G5", 79}, {"G#5", 80}, {"A5", 81}, {"A#5", 82}, {"B5", 83}
		};

		string[] notes = [ "A4", "C5", "E5", "A4", "C5", "E5", "G4", "C5", "E5", "G4", "C5", "E5",
						   "F4", "C5", "E5", "F4", "C5", "E5", "E4", "C5", "E5", "E4", "C5", "E5" ];

		int time = 0;
		var midiTrack = new List<MidiEvent>();
		for (int i = 0; i < notes.Length; i += 3)
		{
			if (noteMapping.TryGetValue(notes[i], out int midiNote1))
			{
				midiTrack.Add(new NoteOnEvent(time, 1, midiNote1, 100, 400));
				midiTrack.Add(new NoteEvent(time + 400, 1, MidiCommandCode.NoteOff, midiNote1, 0));
				time += 400;
			}

			if (noteMapping.TryGetValue(notes[i + 1], out int midiNote2))
			{
				midiTrack.Add(new NoteOnEvent(time, 1, midiNote2, 100, 400));
				midiTrack.Add(new NoteEvent(time + 400, 1, MidiCommandCode.NoteOff, midiNote2, 0));
				time += 400;
			}

			if (noteMapping.TryGetValue(notes[i + 2], out int midiNote3))
			{
				midiTrack.Add(new NoteOnEvent(time, 1, midiNote3, 100, 300));
				midiTrack.Add(new NoteEvent(time + 300, 1, MidiCommandCode.NoteOff, midiNote3, 0));
				time += 300;
			}
		}
		midiTrack.Add(new MetaEvent(MetaEventType.EndTrack, 0, time + 240));

		var track = new MidiEventCollection(1, 500);
		track.AddTrack(midiTrack);
		MidiFile.Export(outputFile, track);
	}
}
