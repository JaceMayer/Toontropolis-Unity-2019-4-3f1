using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public static class Intervals
{
    public static IEnumerator LerpPos(GameObject Gobject, float x = 0f, float y = 0f, float z = 0f, float timeToTake = 1)
    {
        float timeElapsed = 0;
        float Lerpx, Lerpy, Lerpz = 0f;

        while (timeElapsed < timeToTake)
        {
            Lerpx = Mathf.Lerp(Gobject.transform.position.x, x, timeElapsed / timeToTake);
            Lerpy = Mathf.Lerp(Gobject.transform.position.y, y, timeElapsed / timeToTake);
            Lerpz = Mathf.Lerp(Gobject.transform.position.z, z, timeElapsed / timeToTake);
            timeElapsed += Time.deltaTime;

            Gobject.transform.position = new Vector3(Lerpx, Lerpy, Lerpz);
            yield return null;
        }
    }

    public static IEnumerator LerpLocalPos(GameObject Gobject, float x = 0f, float y = 0f, float z = 0f, float timeToTake = 1)
    {
        float timeElapsed = 0;
        float Lerpx, Lerpy, Lerpz = 0f;

        while (timeElapsed < timeToTake)
        {
            Lerpx = Mathf.Lerp(Gobject.transform.localPosition.x, x, timeElapsed / timeToTake);
            Lerpy = Mathf.Lerp(Gobject.transform.localPosition.y, y, timeElapsed / timeToTake);
            Lerpz = Mathf.Lerp(Gobject.transform.localPosition.z, z, timeElapsed / timeToTake);
            timeElapsed += Time.deltaTime;

            Gobject.transform.localPosition = new Vector3(Lerpx, Lerpy, Lerpz);
            yield return null;
        }
    }

    public static IEnumerator LerpScale(GameObject Gobject, float Sx = 0f, float Sy = 0f, float Sz = 0f, float timeToTake = 1)
    {
        float timeElapsed = 0;
        float LerpSx, LerpSy, LerpSz = 0f;

        if(Sz != 0 && Sy != 0 && Sz != 0) Gobject.SetActive(true);

        while (timeElapsed < timeToTake)
        {
            LerpSx = Mathf.Lerp(Gobject.transform.localScale.x, Sx, timeElapsed / timeToTake);
            LerpSy = Mathf.Lerp(Gobject.transform.localScale.y, Sy, timeElapsed / timeToTake);
            LerpSz = Mathf.Lerp(Gobject.transform.localScale.z, Sz, timeElapsed / timeToTake);
            timeElapsed += Time.deltaTime;

            Gobject.transform.localScale = new Vector3(LerpSx, LerpSy, LerpSz);
            yield return null;
        }
        if(Sz == 0 && Sy == 0 && Sz == 0) Gobject.SetActive(false);
    }

    public static IEnumerator LerpRotation(GameObject Gobject, float x = 0f, float y = 0f, float z = 0f, float timeToTake = 1)
    {
        float timeElapsed = 0;
        float LerpX, LerpY, LerpZ, LerpW = 0f;

        while (timeElapsed < timeToTake)
        {
            LerpX = Mathf.Lerp(Gobject.transform.rotation.x, x, timeElapsed / timeToTake);
            LerpY = Mathf.Lerp(Gobject.transform.rotation.y, y, timeElapsed / timeToTake);
            LerpZ = Mathf.Lerp(Gobject.transform.rotation.z, z, timeElapsed / timeToTake);
            timeElapsed += Time.deltaTime;

            Gobject.transform.rotation = Quaternion.Euler(LerpX, LerpY, LerpZ);
            yield return null;
        }
    }

    public static IEnumerator LerpColor(Image Gobject, float R = 0f, float G = 0f, float B = 0f, float A = 0f, float timeToTake = 1)
    {
        float timeElapsed = 0;
        float LerpR, LerpG, LerpB, LerpA = 0f;

        while (timeElapsed < timeToTake)
        {
            LerpR = Mathf.Lerp(Gobject.color.r, R, timeElapsed / timeToTake);
            LerpG = Mathf.Lerp(Gobject.color.g, G, timeElapsed / timeToTake);
            LerpB = Mathf.Lerp(Gobject.color.b, B, timeElapsed / timeToTake);
            LerpA = Mathf.Lerp(Gobject.color.a, A, timeElapsed / timeToTake);
            timeElapsed += Time.deltaTime;

            Gobject.color = new Color(LerpR, LerpG, LerpB, LerpA);
            if(Gobject.gameObject.name == "Bar1") Debug.Log(Gobject.gameObject.name + " - " + Gobject.color);
            yield return null;
        }
    }

    public static IEnumerator LerpCrossAlpha(Image GobjectIn, Image GobjectOut, float timeToTake = 1)
    {
        float timeElapsed = 0;
        float LerpA1, LerpA2 = 0f;

        while (timeElapsed < timeToTake)
        {
            LerpA1 = Mathf.Lerp(GobjectOut.color.a, 0, timeElapsed / timeToTake);
            LerpA2 = Mathf.Lerp(GobjectIn.color.a, 1, timeElapsed / timeToTake);
            timeElapsed += Time.deltaTime;

            GobjectOut.color = new Color(GobjectOut.color.r, GobjectOut.color.g, GobjectOut.color.b, LerpA1);
            GobjectIn.color = new Color(GobjectOut.color.r, GobjectOut.color.g, GobjectOut.color.b, LerpA2);
            yield return null;
        }
    }

    public static IEnumerator LerpAlpha(Text Gobject, float Alpha, float timeToTake = 1)
    {
        float timeElapsed = 0;
        float LerpA = 0f;
        
        if(Alpha != 0){
            Gobject.gameObject.SetActive(true);
        }

        while (timeElapsed < timeToTake)
        {
            LerpA = Mathf.Lerp(Gobject.color.a, Alpha, timeElapsed / timeToTake);
            timeElapsed += Time.deltaTime;

            Gobject.color = new Color(Gobject.color.r, Gobject.color.g, Gobject.color.b, LerpA);
            yield return null;
        }
        if(Alpha == 0){
            Gobject.gameObject.SetActive(false);
        }
    }

    public static IEnumerator LerpAlpha(Image Gobject, float Alpha, float timeToTake = 1)
    {
        float timeElapsed = 0;
        float LerpA = 0f;

        if(Alpha != 0){
            Gobject.gameObject.SetActive(true);
        }

        while (timeElapsed < timeToTake)
        {
            LerpA = Mathf.Lerp(Gobject.color.a, Alpha, timeElapsed / timeToTake);
            timeElapsed += Time.deltaTime;

            Gobject.color = new Color(Gobject.color.r, Gobject.color.g, Gobject.color.b, LerpA);
            yield return null;
        }
        if(Alpha == 0){
            Gobject.gameObject.SetActive(false);
        }
    }

    public static IEnumerator Wait(float t)
    {
       yield return new WaitForSeconds(t);
    }

    public static IEnumerator Func<T>(Action<T> Act, T Param){
        Act(Param);
        yield return 0; // Supresses errors
    }

    public static IEnumerator Func(Action Act){
        Debug.Log("Func");
        Debug.Log(Act);
        Act();
        yield return 0; // Supresses errors
    }

}
