using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class RayShooter : MonoBehaviour
{
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioClip _hitWallSound;
    [SerializeField] private AudioClip _hitEnemySound;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void OnGUI()
    {
        int size = 12;
        float positionX = _camera.pixelWidth / 2 - size / 4;
        float positionY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(positionX, positionY, size, size), "*");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    target.ReactToHit();
                    _soundSource.PlayOneShot(_hitEnemySound);
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                    _soundSource.PlayOneShot(_hitWallSound);
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
