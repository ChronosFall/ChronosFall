namespace ChronosFall.Scripts.Characters
{
    public class CharacterManager
    {
        public int NowCharacterIndex;
        
        public void GetCharacterBaseData(int id)
        {
            
        }
        public int GetNextCharacterIndex()
        {
            return NowCharacterIndex;
        }
        private void SwitchPlayerCharacter(int index)
        {
            NowCharacterIndex = index;
        }

        private void SwitchPreviousPlayerCharacter()
        {
            SwitchPlayerCharacter(NowCharacterIndex--);   
        }

        private void SwitchNextPlayerCharacter()
        {
            SwitchPlayerCharacter(NowCharacterIndex++);
        }
        
    }
}