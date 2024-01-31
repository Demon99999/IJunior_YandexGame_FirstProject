using System.Collections;
using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UI
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        private const string AnonymousEn = "Anonymous";
        private const string AnonymousRu = "Аноним";
        private const string AnonymousTr = "İsimsiz";

        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private Image _image;

        private Coroutine _setImage;

        public void SetData(LeaderboardEntryResponse entry)
        {
            if (entry == null)
                return;

            if (_setImage != null)
                StopCoroutine(_setImage);
            else
                _setImage = StartCoroutine(SetImage(entry.player.profilePicture));

            _playerName.text = IdentifyName(entry.player.publicName);
            _rank.text = entry.rank.ToString();
            _score.text = entry.score.ToString();
        }

        private IEnumerator SetImage(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError
                                                                         || request.result == UnityWebRequest.Result.DataProcessingError)
            {
                throw new System.Exception();
            }

            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            _image.sprite = sprite;
        }

        private string IdentifyName(string publicName)
        {
            string anon = AnonymousEn;

            if (YandexGamesSdk.Environment.i18n.lang == "ru")
            {
                anon = AnonymousRu;
            }
            else if (YandexGamesSdk.Environment.i18n.lang == "tr")
            {
                anon = AnonymousTr;
            }

            if (string.IsNullOrEmpty(publicName))
                publicName = anon;

            return publicName;
        }
    }
}