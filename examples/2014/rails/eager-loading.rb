# The N+1 problem and two fixes: includes (loads associations) vs joins (for filtering only).

# BAD: N+1 — one query for matches, then one per match for the user
matches = Match.where(user_id: current_user.id)
matches.each do |match|
  puts match.other_user.subscription_tier  # fires a query each time
end

# GOOD with includes — two queries total; both user and subscription loaded
matches = Match.includes(other_user: :subscription)
               .where(user_id: current_user.id)
matches.each do |match|
  puts match.other_user.subscription_tier  # reads from memory
end

# GOOD with joins — one query; use when filtering on the association
# (does NOT load association into Ruby objects)
paying_matches = Match.joins(other_user: :subscription)
                      .where(user_id: current_user.id)
                      .where(subscriptions: { tier: 'premium' })
                      .select('matches.*')
