# Contributing Guide

이 프로젝트에 기여해주셔서 감사합니다. 다음 규칙을 따라주세요.

---

## 1. Commit 메시지 규칙 (Conventional Commits)

- `Feat`: 새로운 기능 추가  
- `Fix`: 버그 수정  
- `Docs`: 문서 수정 (코드 변경 없음)  
- `Style`: 코드 포맷팅 (세미콜론 누락 등), 기능 변경 없음  
- `Refactor`: 리팩토링 (기능 변경 없이 구조 개선)  
- `Test`: 테스트 코드 추가 또는 수정  
- `Chore`: 빌드 작업, 패키지 관리, 기타 기타 설정  

### 예시
Feat: 캐릭터 점프 기능 추가
Fix: 몬스터 AI 경로탐색 버그 수정
Docs: README에 설치 방법 추가

---

## 2. 브랜치 명명 규칙

- 브랜치는 Develop 브랜치를 메인으로 진행.
- 진행 당일 일자로 추가 브랜치를 가지고 뭐가 되었든 강의 마감 6시에 풀리퀘스트를 진행.
- 풀 리퀘스트 이후에 다음날 브랜치를 곧장 만들어서 프로젝트 진행함.

---

## 3. 코드 스타일 가이드 (C# / Unity)

- 클래스명: `PascalCase`
- 변수명 및 메서드명: `camelCase`
- 상수: `UPPER_SNAKE_CASE`
- 함수는 하나의 책임만 갖도록 작성
- Unity의 MonoBehaviour는 `Awake`, `Start`, `Update` 순서로 배치
- `SerializeField`를 통해 인스펙터 노출, `private` 유지
- `public` 필드 최소화, `Property` 사용 권장

---

## 4. Pull Request(PR) 가이드

- PR 제목: 작업 목적을 명확히 작성
- PR 본문: 변경사항 요약, 테스트 여부, 참고 이슈 등 명시

---

## 5. 이슈 작성 가이드

- 버그: `Bug` 라벨
- 기능 요청: `Feature` 라벨
- 문서 관련: `Documentation` 라벨

각 이슈는 다음 내용을 포함해주세요:
- 문제 설명 또는 요청 배경
- 재현 방법 (가능한 경우)
- 기대 결과

---

감사합니다!