Shader "BlurOnPlane" {

    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur", Range(0, 0.05)) = 0.0
        _Brightness ("Brightness", Range(0, 10)) = 5
        _BlurForceRangeInt ("BlurForceRangeInt", Range(1, 16)) = 4
    }

    SubShader {

        Pass {
        
            CGPROGRAM

            #pragma vertex vert  
            #pragma fragment frag

#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex;
float _BlurAmount;
float _Brightness;
float _BlurForceRangeInt;
            
v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}
            
fixed4 frag(v2f i) : SV_Target
{
    int pixelCount = _BlurForceRangeInt;
    // Вычисляем смещение
    fixed offset = _BlurAmount / pixelCount;
    
    fixed4 col = fixed4(0, 0, 0, 0);

    // Цикл по X
    for (int x = -pixelCount; x <= pixelCount; x++)
    {

        // Цикл по Y
        for (int y = -pixelCount; y <= pixelCount; y++)
        {

            // Текущие смещения
            fixed2 uvOffset = fixed2(x, y) * offset;

            // Сэмплируем текстуру
            fixed4 pixelColor = tex2D(_MainTex, i.uv + uvOffset);

            // Накапливаем значение
            col += pixelColor;
        }
    }

    // Нормализуем результат
    col /= pow(_Brightness, pixelCount);

    return col;
}

            ENDCG
        }
    }
}