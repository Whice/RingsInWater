Shader "UI/Transparent Image" {

    Properties{
      [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
      _Opacity("Opacity", Range(0,1)) = 1
    }

        SubShader{

          Tags {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
          }

          Cull Off
          Lighting Off
          ZWrite Off
          Blend SrcAlpha OneMinusSrcAlpha

          Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed _Opacity;

            struct appdata_t {
              float4 vertex : POSITION;
              float2 texcoord : TEXCOORD0;
              fixed4 color : COLOR;
            };

            struct v2f {
              float4 vertex : SV_POSITION;
              half2 texcoord : TEXCOORD0;
              fixed4 color : COLOR;
            };

            v2f vert(appdata_t v) {
              v2f o;
              o.vertex = UnityObjectToClipPos(v.vertex);
              o.texcoord = v.texcoord;
              o.color = v.color;
              return o;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target {
              fixed4 col = tex2D(_MainTex, i.texcoord) * i.color;
              col.a *= _Opacity;
              return col;
            }

            ENDCG
          }
      }
}