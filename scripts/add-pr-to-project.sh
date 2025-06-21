#!/bin/bash
set -euo pipefail

PROJECT_URL="$1"
TOKEN="$GITHUB_TOKEN"
EVENT_PATH="${GITHUB_EVENT_PATH}" # path to event JSON

if [[ "$PROJECT_URL" =~ /(orgs|users)/([^/]+)/projects/([0-9]+) ]]; then
  OWNER_TYPE="${BASH_REMATCH[1]}"
  OWNER_NAME="${BASH_REMATCH[2]}"
  PROJECT_NUMBER="${BASH_REMATCH[3]}"
else
  echo "Invalid project URL: $PROJECT_URL" >&2
  exit 1
fi

OWNER_FIELD=$( [ "$OWNER_TYPE" = "orgs" ] && echo "organization" || echo "user" )

PR_NODE_ID=$(jq -r '.pull_request.node_id' "$EVENT_PATH")

PROJECT_ID=$(curl -s -H "Authorization: Bearer $TOKEN" \
  -X POST -H "Content-Type: application/json" \
  -d "{\"query\":\"query(\$owner:String!,\$number:Int!){ $OWNER_FIELD(login:\$owner){ projectV2(number:\$number){ id } } }\",\"variables\":{\"owner\":\"$OWNER_NAME\",\"number\":$PROJECT_NUMBER}}" https://api.github.com/graphql | jq -r ".data.$OWNER_FIELD.projectV2.id")

curl -s -H "Authorization: Bearer $TOKEN" \
  -X POST -H "Content-Type: application/json" \
  -d "{\"query\":\"mutation(\$project:ID!,\$content:ID!){ addProjectV2ItemById(input:{projectId:\$project, contentId:\$content}){item{id}} }\",\"variables\":{\"project\":\"$PROJECT_ID\",\"content\":\"$PR_NODE_ID\"}}" https://api.github.com/graphql > /dev/null

echo "Pull request added to project"
